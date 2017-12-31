namespace ProductBuilder.Application.AutoMapper.Extensions
{
    using Asd.Domain.Core.Commands;
    using Asd.Domain.Core.Models;
    using Asd.Domain.Interfaces;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using ProductBuilder.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

    public static class AggregateExtensions
    {
        public static string ToAggregateCode (this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));
            
            var aggregateClassMembers = new List<MemberDeclarationSyntax>();

            // like -> public string Foo { get; set; }
            aggregate.AggregateProperties
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    var propertyDeclaration = x.Type == "Guid" ? 
                        PropertyDeclaration(NullableType(IdentifierName(x.Type)), Identifier(x.Name)) : 
                        PropertyDeclaration(IdentifierName(x.Type), Identifier(x.Name));

                    propertyDeclaration = propertyDeclaration
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[]
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        })));

                    aggregateClassMembers.Add(propertyDeclaration);
                });

            // like -> public ICollection<Foo> Foos { get; set; }
            aggregate.LinkedAggregateProperties
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x =>
                {
                    var propertyType = TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName(x.Aggregate?.Name)));
                    var propertyDeclaration = PropertyDeclaration(GenericName(Identifier("ICollection")).WithTypeArgumentList(propertyType), Identifier(x.Aggregate?.NamePluralized))
                        .WithModifiers(TokenList(new[]
                        {
                            Token(SyntaxKind.PublicKeyword),
                            Token(SyntaxKind.VirtualKeyword)
                        }))
                        .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[] 
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        })));
                    aggregateClassMembers.Add(propertyDeclaration);
                });

            var assignIdExpression = GetAssignmentExpression("Id", "id");
            var throwNewArgumentNullExceptionNameofId = GetThrowNewArgumentNullExceptionStatement("id");
            var guidEmpty = GetSimpleMemberAccessExpression(IdentifierName(nameof(Guid)), IdentifierName(nameof(Guid.Empty)));
            var ifIdEqualityGuidEmpty = GetIfEqualsStatement(IdentifierName("id"), guidEmpty, throwNewArgumentNullExceptionNameofId);
            var publicConstructorParameters = ParameterList(SingletonSeparatedList(Parameter(Identifier("id")).WithType(IdentifierName(nameof(Guid)))));
            var publicConstructor = GetPublicConstructorWithThisConstructorInitializer(aggregate.Name, publicConstructorParameters, Block(ifIdEqualityGuidEmpty, assignIdExpression));
            var protectedConstructorBodyStatements = aggregate.LinkedAggregateProperties
                .Where(x => !x.Deleted)
                .Select(x => ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(x.Aggregate.NamePluralized), ObjectCreationExpression(GenericName(Identifier("HashSet"))
                    .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName(x.Aggregate.Name)))))
                    .WithArgumentList(ArgumentList()))));
            var protectedConstructor = GetProtectedConstructor(aggregate.Name, Block(protectedConstructorBodyStatements));
            
            aggregateClassMembers.Add(publicConstructor);
            aggregateClassMembers.Add(protectedConstructor);

            var aggregateClass = GetPublicClassWithBaseList(aggregate.Name, GetBaseList(nameof(AsdEntity)));
            var aggregateNamespace = GetNamespace($"{aggregate.Product?.Title}.Domain.Core.Models")
                .WithUsings(GetUsings("Asd.Domain.Core.Models", "System", "System.Collections.Generic", $"{aggregate.Product?.Title}.Domain.Models"))
                .WithMembers(GetMembers(aggregateClass.WithMembers(List(aggregateClassMembers))));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToBaseCommandCode(this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var baseCommandClassMembers = new List<MemberDeclarationSyntax>();

            // like => public string Foo { get; protected set; }
            aggregate.AggregateProperties
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    var propertyDeclaration = PropertyDeclaration(IdentifierName(x.Type), Identifier(x.Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[]
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithModifiers(TokenList(Token(SyntaxKind.ProtectedKeyword))).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        })));
                    baseCommandClassMembers.Add(propertyDeclaration);
                });

            var baseCommandClass = GetPublicAbstractClassWithBaseList($"{aggregate.Name}Command", GetBaseList(nameof(AsdCommand)));
            var baseCommandNamespace = GetNamespace($"{aggregate.Product?.Title}.Domain.Commands.Product.Base")
                .WithUsings(GetUsings($"{aggregate.Product?.Title}.Domain.Core.Commands", "System"))
                .WithMembers(GetMembers(baseCommandClass.WithMembers(List(baseCommandClassMembers))));

            return CompilationUnit()
                .WithMembers(GetMembers(baseCommandNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToAggregateValidatorCode(this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));
            
            var aggregateValidatorClass = GetPublicGenericClass($"{aggregate.Name}Validator", "T", GetGenericBaseList("AbstractValidator", "T"), $"{aggregate.Name}Command");
            var aggregateValidatorNamespace = GetNamespace($"{aggregate.Product?.Title}.Domain.Validations.Product")
                .WithUsings(GetUsings($"{aggregate.Product?.Title}.Domain.Commands.Product.Base", "FluentValidation"))
                .WithMembers(GetMembers(aggregateValidatorClass));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateValidatorNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToIAggregateRepositoryCode(this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var aggregateRepositoryInterface = GetPublicInterfaceWithGenericBaseList($"I{aggregate.Name}Repository", GetGenericBaseList("IAsdRepository", aggregate.Name));
            var aggregateRepositoryInterfaceNamespace = GetNamespace($"{aggregate.Product?.Title}.Domain.Interfaces")
                .WithUsings(GetUsings($"{aggregate.Product?.Title}.Domain.Interfaces", $"{aggregate.Product?.Title}.Domain.Models"))
                .WithMembers(GetMembers(aggregateRepositoryInterface));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateRepositoryInterfaceNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToAggregateRepositoryCode(this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var aggregateRepositoryClass = GetAggregateRepositoryClass(aggregate.Name);
            var aggregateRepositoryClassNamespace = GetNamespace($"{aggregate.Product?.Title}.Infra.Data.Repository")
                .WithUsings(GetUsings("Asd.Infra.Data.Context", "Asd.Infra.Data.Repository", $"{aggregate.Product?.Title}.Domain.Interfaces", $"{aggregate.Product?.Title}.Domain.Models"))
                .WithMembers(GetMembers(aggregateRepositoryClass));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateRepositoryClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        private static SyntaxList<UsingDirectiveSyntax> AddUsings2(params string[] usings)
        {
            if (usings == null)
                throw new ArgumentNullException(nameof(usings));

            return List(usings.Select(x => UsingDirective(IdentifierName(x))));
        }
        
        private static NamespaceDeclarationSyntax CreateNamespace(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentNullException(nameof(@namespace));
            return SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(@namespace)).NormalizeWhitespace();
        }

        private static NamespaceDeclarationSyntax AddUsings(this NamespaceDeclarationSyntax namespaceDeclaration, params string[] usings)
        {
            if (namespaceDeclaration == null)
                throw new ArgumentNullException(nameof(namespaceDeclaration));
            if (usings == null)
                throw new ArgumentNullException(nameof(usings));
            return namespaceDeclaration.AddUsings(usings.Select(x => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(x))).ToArray());
        }

        private static ClassDeclarationSyntax CreatePublicClass(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentNullException(nameof(className));
            return SyntaxFactory.ClassDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
        }

        private static ClassDeclarationSyntax CreatePublicAbstractClass(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            return SyntaxFactory.ClassDeclaration(identifier)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.AbstractKeyword));
        }
        
        private static ClassDeclarationSyntax AddSimpleBaseTypes(this ClassDeclarationSyntax classDeclaration, params string[] simpleBaseTypes)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (simpleBaseTypes == null)
                throw new ArgumentNullException(nameof(simpleBaseTypes));
            return classDeclaration.AddBaseListTypes(simpleBaseTypes.Select(x => SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(x))).ToArray());
        }

        private static ClassDeclarationSyntax AddPublicProperty(this ClassDeclarationSyntax classDeclaration, string type, string identifier)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            return classDeclaration.AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type), identifier)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))));
        }

        private static ClassDeclarationSyntax AddPublicPropertyWithProtectedSetter(this ClassDeclarationSyntax classDeclaration, string type, string identifier)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            return classDeclaration.AddMembers(SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type), identifier)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)))
                .AddAccessorListAccessors(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword))
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))));
        }

        private static ClassDeclarationSyntax AddPublicProperties(this ClassDeclarationSyntax classDeclaration, IDictionary<string, string> identifierAndTypes)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (identifierAndTypes == null)
                throw new ArgumentNullException(nameof(identifierAndTypes));
            identifierAndTypes
                .ToList()
                .ForEach(x => classDeclaration = classDeclaration.AddPublicProperty(x.Value, x.Key));
            return classDeclaration;
        }

        private static ClassDeclarationSyntax AddPublicPropertiesWithProtectedSetter(this ClassDeclarationSyntax classDeclaration, IDictionary<string, string> identifierAndTypes)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (identifierAndTypes == null)
                throw new ArgumentNullException(nameof(identifierAndTypes));
            identifierAndTypes.ToList()
                .ForEach(x => classDeclaration = classDeclaration.AddPublicPropertyWithProtectedSetter(x.Value, x.Key));
            return classDeclaration;
        }

        private static ClassDeclarationSyntax AddTypeOf(this ClassDeclarationSyntax classDeclaration, string identifier)
        {
            if (classDeclaration == null)
                throw new ArgumentNullException(nameof(classDeclaration));
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            return classDeclaration.WithTypeParameterList(SyntaxFactory.TypeParameterList(SyntaxFactory.SingletonSeparatedList(SyntaxFactory.TypeParameter(SyntaxFactory.Identifier(identifier)))));
        }

        // ---------------------------------

        private static ExpressionStatementSyntax GetAssignmentExpression(string leftExpression, string rightExpression)
        {
            if (string.IsNullOrWhiteSpace(leftExpression))
                throw new ArgumentNullException(nameof(leftExpression));
            return ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(leftExpression), IdentifierName(rightExpression)));
        }

        private static ThrowStatementSyntax GetThrowNewArgumentNullExceptionStatement(string nameOf)
        {
            if (string.IsNullOrWhiteSpace(nameOf))
                throw new ArgumentNullException(nameof(nameOf));
            return ThrowStatement(ObjectCreationExpression(IdentifierName(nameof(ArgumentNullException)))
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName(nameOf))))))))));
        }

        private static IfStatementSyntax GetIfEqualsStatement(ExpressionSyntax leftExpression, ExpressionSyntax rightExpression, StatementSyntax statement)
        {
            if (leftExpression == null)
                throw new ArgumentNullException(nameof(rightExpression));
            if (statement == null)
                throw new ArgumentNullException(nameof(statement));
            return IfStatement(BinaryExpression(SyntaxKind.EqualsExpression, leftExpression, rightExpression), statement);
        }

        private static MemberAccessExpressionSyntax GetSimpleMemberAccessExpression(ExpressionSyntax expression, SimpleNameSyntax name)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            return MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, name);
        }

        private static ConstructorDeclarationSyntax GetPublicConstructorWithThisConstructorInitializer(string identifier, ParameterListSyntax parameters, BlockSyntax body)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            return ConstructorDeclaration(Identifier(identifier))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(parameters)
                .WithInitializer(ConstructorInitializer(SyntaxKind.ThisConstructorInitializer, ArgumentList()))
                .WithBody(body);
        }

        private static ConstructorDeclarationSyntax GetProtectedConstructor(string identifier, BlockSyntax body)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            return ConstructorDeclaration(Identifier(identifier))
                .WithModifiers(TokenList(Token(SyntaxKind.ProtectedKeyword)))
                .WithBody(body);
        }

        private static ClassDeclarationSyntax GetPublicClassWithBaseList(string identifier, BaseListSyntax baseList)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (baseList == null)
                throw new ArgumentNullException(nameof(baseList));
            return ClassDeclaration(identifier)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(baseList);
        }

        private static ClassDeclarationSyntax GetPublicAbstractClassWithBaseList(string identifier, BaseListSyntax baseList)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (baseList == null)
                throw new ArgumentNullException(nameof(baseList));

            return ClassDeclaration(identifier)
                .WithModifiers(TokenList(new[] 
                {
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.AbstractKeyword)
                }))
                .WithBaseList(baseList);
        }

        private static ClassDeclarationSyntax GetPublicGenericClass(string identifier, string type, BaseListSyntax genericBaseList, string baseClassName)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(baseClassName))
                throw new ArgumentNullException(nameof(baseClassName));
            return ClassDeclaration(identifier)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithTypeParameterList(TypeParameterList(SingletonSeparatedList(TypeParameter(Identifier(type)))))
                .WithBaseList(genericBaseList)
                .WithConstraintClauses(SingletonList(TypeParameterConstraintClause(IdentifierName(type))
                .WithConstraints(SingletonSeparatedList<TypeParameterConstraintSyntax>(TypeConstraint(IdentifierName(baseClassName))))));
        }

        private static ClassDeclarationSyntax GetAggregateRepositoryClass(string aggregateName)
        {
            if (string.IsNullOrWhiteSpace(aggregateName))
                throw new ArgumentNullException(nameof(aggregateName));
            return ClassDeclaration($"{aggregateName}Repository")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(GetAggregateRepositoryBaseList(aggregateName))
                .WithMembers(SingletonList<MemberDeclarationSyntax>(GetAggregateRepositoryConstructor(aggregateName)));
        }

        private static BaseListSyntax GetAggregateRepositoryBaseList(string aggregateName)
        {
            if (string.IsNullOrWhiteSpace(aggregateName))
                throw new ArgumentNullException(nameof(aggregateName));
            var typeArgumentList = TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName(aggregateName)));
            return BaseList(SeparatedList<BaseTypeSyntax>(new SyntaxNodeOrToken[]
            {
                SimpleBaseType(GenericName(Identifier("AsdRepository")).WithTypeArgumentList(typeArgumentList)),
                Token(SyntaxKind.CommaToken),
                SimpleBaseType(IdentifierName($"I{aggregateName}Repository"))
            }));
        }

        private static ConstructorDeclarationSyntax GetAggregateRepositoryConstructor(string aggregateName)
        {
            if (string.IsNullOrWhiteSpace(aggregateName))
                throw new ArgumentNullException(nameof(aggregateName));
            return ConstructorDeclaration(Identifier($"{aggregateName}Repository"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("context")).WithType(IdentifierName("AsdSqlContext")))))
                .WithInitializer(ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SingletonSeparatedList(Argument(IdentifierName("context"))))))
                .WithBody(Block());
        }

        private static InterfaceDeclarationSyntax GetPublicInterfaceWithGenericBaseList(string identifier, BaseListSyntax genericBaseList)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            if (genericBaseList == null)
                throw new ArgumentNullException(nameof(genericBaseList));
            return InterfaceDeclaration(identifier)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(genericBaseList);
        }

        private static NamespaceDeclarationSyntax GetNamespace(string @namespace)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
                throw new ArgumentNullException(nameof(@namespace));
            return NamespaceDeclaration(ParseName(@namespace));
        }

        private static SyntaxList<UsingDirectiveSyntax> GetUsings(params string[] usings)
        {
            if (usings == null)
                throw new ArgumentNullException(nameof(usings));
            return List(usings.Select(x => UsingDirective(IdentifierName(x))));
        }

        private static SyntaxList<MemberDeclarationSyntax> GetMembers(MemberDeclarationSyntax node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            return SingletonList(node);
        }

        private static BaseListSyntax GetBaseList(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentNullException(nameof(identifier));
            return BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(identifier))));
        }

        private static BaseListSyntax GetGenericBaseList(string baseClassName, string type)
        {
            if (string.IsNullOrWhiteSpace(baseClassName))
                throw new ArgumentNullException(nameof(baseClassName));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            var argumentTypeList = TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName(type)));
            var simpleBaseType = SimpleBaseType(GenericName(Identifier(baseClassName)).WithTypeArgumentList(argumentTypeList));
            return BaseList(SingletonSeparatedList<BaseTypeSyntax>(simpleBaseType));
        }
    }
}