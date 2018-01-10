namespace ProductBuilder.Application.AutoMapper.Extensions
{
    using Asd.Domain.Core.Commands;
    using Asd.Domain.Core.Events;
    using Asd.Domain.Core.Models;
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

        public static string ToDomainEventCode(this Event aggregateEvent)
        {
            if (aggregateEvent == null)
                throw new ArgumentNullException(nameof(aggregateEvent));

            var domainEventClass = GetDomainEventClass(aggregateEvent.EventName);
            var domainEventClassNamespace = GetNamespace($"{aggregateEvent.Aggregate?.Product?.Title}.Domain.Events.{aggregateEvent.Aggregate.Name}")
                .WithUsings(GetUsings("Asd.Domain.Core.Events", $"{aggregateEvent.Aggregate?.Product?.Title}.Domain.Models", "System"))
                .WithMembers(GetMembers(domainEventClass));

            return CompilationUnit()
                .WithMembers(GetMembers(domainEventClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToDomainCommandCode(this Command domainCommand)
        {
            if (domainCommand == null)
                throw new ArgumentNullException(nameof(domainCommand));

            var domainCommandClass = GetDomainCommandClass(domainCommand);
            var domainCommandClassNamespace = GetNamespace($"{domainCommand.Aggregate?.Product?.Title}.Domain.Commands.{domainCommand.Aggregate?.Name}")
                .WithUsings(GetUsings($"{domainCommand.Aggregate?.Product?.Title}.Domain.Commands.{domainCommand.Aggregate?.Name}.Base", $"{domainCommand.Aggregate?.Product?.Title}.Domain.Validations.{domainCommand.Aggregate?.Name}"))
                .WithMembers(GetMembers(domainCommandClass));

            return CompilationUnit()
                .WithMembers(GetMembers(domainCommandClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToAggregateEventHandlerCode(this Aggregate aggregate)
        {
            if (aggregate == null)
                throw new ArgumentNullException(nameof(aggregate));

            var aggregateEventHandlerClass = GetAggregateEventHandlerClass(aggregate);
            var aggregateEventHandlerClassNamespace = GetNamespace($"{aggregate.Product?.Title}.Domain.EventHandlers")
                .WithUsings(GetUsings("Asd.Domain.Core.Events"));

            if (aggregate.Events.Count > 0)
                aggregateEventHandlerClassNamespace = aggregateEventHandlerClassNamespace
                    .WithUsings(GetUsings($"{aggregate.Product?.Title}.Domain.Events.{aggregate.Name}"));

            aggregateEventHandlerClassNamespace = aggregateEventHandlerClassNamespace
                .WithMembers(GetMembers(aggregateEventHandlerClass));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateEventHandlerClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToAggregateCommandHandlerCode(this Aggregate domainAggregate)
        {
            if (domainAggregate == null)
                throw new ArgumentNullException(nameof(domainAggregate));

            var aggregateCommandHandlerClass = GetAggregateCommandHandlerClass(domainAggregate);
            var aggregateCommandHandlerClassNamespace = GetNamespace($"{domainAggregate.Product?.Title}.Domain.CommandHandlers")
                .WithUsings(GetUsings("Asd.Domain.CommandHandler", 
                    "Asd.Domain.Core.Bus", 
                    "Asd.Domain.Core.Events", 
                    "Asd.Domain.Core.Notifications",
                    "Asd.Domain.Interfaces", 
                    $"{domainAggregate.Product?.Title}.Domain.Commands.{domainAggregate.Name}",
                    $"{domainAggregate.Product?.Title}.Domain.Events.{domainAggregate.Name}",
                    $"{domainAggregate.Product?.Title}.Domain.Interfaces",
                    $"{domainAggregate.Product?.Title}.Domain.Models",
                    "System"))
                .WithMembers(GetMembers(aggregateCommandHandlerClass));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateCommandHandlerClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToApiViewModelCode(this Command domainCommand)
        {
            if (domainCommand == null)
                throw new ArgumentNullException(nameof(domainCommand));

            var properties = new List<MemberDeclarationSyntax>()
            {
                PropertyDeclaration(
                        IdentifierName("Guid"),
                        Identifier("Id"))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[]
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        })))
            };

            domainCommand.DomainCommandArguments
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    properties.Add(PropertyDeclaration(
                        IdentifierName(x.AggregateProperty.Type),
                        Identifier(x.AggregateProperty.Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[]
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        }))));
                });

            var aggregateRoot = domainCommand.DomainCommandArguments
                .Where(x => !x.Deleted && x.AggregateProperty.IsAggregateRoot)
                .SingleOrDefault()?.AggregateProperty;
            if(aggregateRoot == null)
            {
                aggregateRoot = domainCommand.Aggregate.AggregateProperties
                    .Where(y => !y.Deleted && y.IsAggregateRoot)
                    .SingleOrDefault();

                if(aggregateRoot != null)
                    properties.Add(PropertyDeclaration(
                        IdentifierName(aggregateRoot.Type),
                        Identifier(aggregateRoot.Name))
                            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                            .WithAccessorList(AccessorList(List(new AccessorDeclarationSyntax[]
                            {
                                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        }))));
            }

            var apiViewModelClass = ClassDeclaration($"{domainCommand.CommandName}ApiViewModel")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithMembers(List(properties));
            var apiViewModelClassNamespace = GetNamespace($"{domainCommand.Aggregate?.Product?.Title}.Application.ViewModels.{domainCommand.CommandName}Api")
                .WithUsings(GetUsings("System"))
                .WithMembers(GetMembers(apiViewModelClass));

            return CompilationUnit()
                .WithMembers(GetMembers(apiViewModelClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToIAggregateAppServiceInterfaceCode(this Aggregate domainAggregate)
        {
            if (domainAggregate == null)
                throw new ArgumentNullException(nameof(domainAggregate));

            var commands = new List<MemberDeclarationSyntax>();
            domainAggregate.Commands
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    commands.Add(MethodDeclaration(PredefinedType(Token(
                        SyntaxKind.VoidKeyword)), 
                        Identifier(x.CommandName))
                            .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("model"))
                            .WithType(IdentifierName($"{x.CommandName}ApiViewModel")))))
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
                });

            var iAggregateAppServiceInterface = InterfaceDeclaration($"I{domainAggregate.Name}AppService")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName("IDisposable")))))
                .WithMembers(List(commands));
            var iAggregateAppServiceInterfaceNamespace = GetNamespace($"{domainAggregate.Product?.Title}.Application.Interfaces")
                .WithUsings(GetUsings("System"))
                .WithMembers(GetMembers(iAggregateAppServiceInterface));
            if (commands.Count > 0)
                iAggregateAppServiceInterfaceNamespace = iAggregateAppServiceInterfaceNamespace
                    .WithUsings(GetUsings($"{domainAggregate.Product?.Title}.Application.ViewModels.{domainAggregate.Name}Api"));

            return CompilationUnit()
                .WithMembers(GetMembers(iAggregateAppServiceInterfaceNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

        public static string ToAggregateAppServiceCode(this Aggregate domainAggregate)
        {
            if (domainAggregate == null)
                throw new ArgumentNullException(nameof(domainAggregate));

            var classMembers = new List<MemberDeclarationSyntax>()
            {
                // private readonly IFooRepository _repository;
                FieldDeclaration(VariableDeclaration(IdentifierName($"I{domainAggregate.Name}Repository"))
                    .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("_repository")))))
                        .WithModifiers(TokenList(new []
                        {
                            Token(SyntaxKind.PrivateKeyword),
                            Token(SyntaxKind.ReadOnlyKeyword)
                        })),
                // public FooAppService (IAsdBus bus, IMapper mapper, IFooRepository repository)
                ConstructorDeclaration(Identifier($"{domainAggregate.Name}AppService"))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(new SyntaxNodeOrToken[]
                    {
                        Parameter(Identifier("bus"))
                            .WithType(IdentifierName("IAsdBus")),
                        Token(SyntaxKind.CommaToken),
                        Parameter(Identifier("mapper"))
                            .WithType(IdentifierName("IMapper")),
                        Token(SyntaxKind.CommaToken),
                        Parameter(Identifier("repository"))
                            .WithType(IdentifierName($"I{domainAggregate.Name}Repository"))
                    })))
                    // : base(bus, mapper)
                    .WithInitializer(ConstructorInitializer(
                        SyntaxKind.BaseConstructorInitializer,
                        ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                        {
                            Argument(IdentifierName("bus")),
                            Token(SyntaxKind.CommaToken),
                            Argument(IdentifierName("mapper"))
                        }))))
                    // { if (repository == null) throw new ArgumentNullException(nameof(repository)); _repository = repository; }
                    .WithBody(Block(IfStatement(BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        IdentifierName("repository"),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                            ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("repository"))))))))))),
                        ExpressionStatement(AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("_repository"),
                            IdentifierName("repository")))))

            };

            domainAggregate.Commands
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    switch (x.CommandType.ToLower())
                    {
                        case "create":
                            classMembers.Add(MethodDeclaration(PredefinedType(Token(
                                SyntaxKind.VoidKeyword)), 
                                Identifier(x.CommandName))
                                    .WithModifiers(TokenList(Token(
                                        SyntaxKind.PublicKeyword)))
                                            .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("model"))
                                                .WithType(IdentifierName($"{x.CommandName}ApiViewModel")))))
                                                    .WithBody(Block(IfStatement(BinaryExpression(
                                                        SyntaxKind.EqualsExpression, 
                                                        IdentifierName("model"), 
                                                        LiteralExpression(SyntaxKind.NullLiteralExpression)), 
                                                            ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                                                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("model"))))))))))), 
                                                                        IfStatement(BinaryExpression(
                                                                            SyntaxKind.EqualsExpression, 
                                                                            MemberAccessExpression(
                                                                                SyntaxKind.SimpleMemberAccessExpression, 
                                                                                IdentifierName("model"), 
                                                                                IdentifierName("Id")), 
                                                                            MemberAccessExpression(
                                                                                SyntaxKind.SimpleMemberAccessExpression, 
                                                                                IdentifierName("Guid"),
                                                                                IdentifierName("Empty"))), 
                                                                                ExpressionStatement(AssignmentExpression(
                                                                                    SyntaxKind.SimpleAssignmentExpression, 
                                                                                    MemberAccessExpression(
                                                                                        SyntaxKind.SimpleMemberAccessExpression, 
                                                                                        IdentifierName("model"), 
                                                                                        IdentifierName("Id")), 
                                                                                    InvocationExpression(MemberAccessExpression(
                                                                                        SyntaxKind.SimpleMemberAccessExpression, 
                                                                                        IdentifierName("Guid"), 
                                                                                        IdentifierName("NewGuid")))))), 
                                                                        ExpressionStatement(InvocationExpression(MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression, 
                                                                            IdentifierName("Bus"), 
                                                                            IdentifierName("SendCommand")))
                                                                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(MemberAccessExpression(
                                                                                    SyntaxKind.SimpleMemberAccessExpression, 
                                                                                    IdentifierName("Mapper"), 
                                                                                    GenericName(Identifier("Map"))
                                                                                        .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName($"{x.CommandName}Command"))))))
                                                                                            .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("model")))))))))))));
                            break;
                        default:
                            classMembers.Add(MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), 
                                Identifier(x.CommandName))
                                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                                    .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("model"))
                                        .WithType(IdentifierName($"{x.CommandName}ApiViewModel")))))
                                    .WithBody(Block(IfStatement(BinaryExpression(
                                        SyntaxKind.EqualsExpression,
                                        IdentifierName("model"), 
                                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                        ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                                            .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("model"))))))))))),
                                                ExpressionStatement(InvocationExpression(MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression, 
                                                    IdentifierName("Bus"), 
                                                    IdentifierName("SendCommand")))
                                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("Mapper"),
                                                            GenericName(Identifier("Map"))
                                                                .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName($"{x.CommandName}Command"))))))
                                                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("model")))))))))))));
                            break;
                    }
                });

            classMembers.Add(MethodDeclaration(PredefinedType(
                Token(SyntaxKind.VoidKeyword)),
                Identifier("Dispose"))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithBody(Block(SingletonList<StatementSyntax>(ExpressionStatement(InvocationExpression(MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("GC"),
                            IdentifierName("SuppressFinalize")))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(ThisExpression())))))))));

            var aggregateAppServiceClass = ClassDeclaration($"{domainAggregate.Name}AppService")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(BaseList(SeparatedList<BaseTypeSyntax>(new SyntaxNodeOrToken[]
                {
                    SimpleBaseType(IdentifierName("AsdAppService")),Token(SyntaxKind.CommaToken),
                    SimpleBaseType(IdentifierName($"I{domainAggregate.Name}AppService"))
                })))
                .WithMembers(List(classMembers));
            var aggregateAppServiceClassNamespace = GetNamespace($"{domainAggregate.Product?.Title}.Application.Services")
                .WithUsings(GetUsings(
                    "Asd.Application.Services",
                    "Asd.Domain.Core.Bus",
                    "global::AutoMapper",
                    "System",
                    "System.Linq",
                    $"{domainAggregate.Product?.Title}.Application.Interfaces",
                    $"{domainAggregate.Product?.Title}.Domain.Interfaces"))
                .WithMembers(GetMembers(aggregateAppServiceClass));

            if (domainAggregate.Commands.Count > 0)
                aggregateAppServiceClassNamespace = aggregateAppServiceClassNamespace
                    .WithUsings(GetUsings(
                        $"{domainAggregate.Product?.Title}.Domain.Commands.{domainAggregate.Name}",
                        $"{domainAggregate.Product?.Title}.Application.ViewModels.{domainAggregate.Name}Api"));

            return CompilationUnit()
                .WithMembers(GetMembers(aggregateAppServiceClassNamespace))
                .NormalizeWhitespace()
                .ToFullString();
        }

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

        private static ClassDeclarationSyntax GetDomainEventClass(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));

            return ClassDeclaration($"{eventName}Event")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(GetDomainEventBaseList())
                .WithMembers(SingletonList<MemberDeclarationSyntax>(GetDomainEventConstructor(eventName)));
        }

        private static ClassDeclarationSyntax GetDomainCommandClass(Command domainCommand)
        {
            if (domainCommand == null)
                throw new ArgumentNullException(nameof(domainCommand));

            return ClassDeclaration($"{domainCommand.CommandName}Command")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName($"{domainCommand.Aggregate?.Name}Command")))))
                .WithMembers(List(new MemberDeclarationSyntax[]
                {
                    GetDomainCommandClassConstructor($"{domainCommand.CommandName}Command", domainCommand.DomainCommandArguments),
                    GetDomainCommandClassIsValidMethod(domainCommand.Aggregate?.Name)
                }));
        }

        private static MethodDeclarationSyntax GetDomainCommandClassIsValidMethod(string domainAggregateName)
        {
            if (string.IsNullOrWhiteSpace(domainAggregateName))
                throw new ArgumentNullException(nameof(domainAggregateName));

            var returnStatement = ReturnStatement(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("ValidationResult"), IdentifierName("IsValid")));
            var thisExpression = ArgumentList(SingletonSeparatedList(Argument(ThisExpression())));

            var domainCommandArgumentValidatorOfDomainCommandArgumentValidator = GenericName(Identifier($"{domainAggregateName}Validator"))
                .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName($"{domainAggregateName}Command"))));
            var domainCommandValidatorCreationExpression = ObjectCreationExpression(domainCommandArgumentValidatorOfDomainCommandArgumentValidator)
                .WithArgumentList(ArgumentList());
            var invocationExpression = InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, domainCommandValidatorCreationExpression, IdentifierName("Validate")));
            var assignmentExpression = AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("ValidationResult"), invocationExpression.WithArgumentList(thisExpression));

            return MethodDeclaration(PredefinedType(Token(SyntaxKind.BoolKeyword)), Identifier("IsValid"))
                .WithModifiers(TokenList(new[]
                {
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.OverrideKeyword)
                }))
                .WithBody(Block(ExpressionStatement(assignmentExpression), returnStatement));
        }

        private static ConstructorDeclarationSyntax GetDomainCommandClassConstructor(string constructorIdentifier, IEnumerable<DomainCommandArgument> domainCommandArguments)
        {
            if (domainCommandArguments == null)
                throw new ArgumentNullException(nameof(domainCommandArguments));
            if (string.IsNullOrWhiteSpace(constructorIdentifier))
                throw new ArgumentNullException(nameof(constructorIdentifier));

            var commandArgs = new List<SyntaxNodeOrToken>
            {
                Parameter(Identifier("id")).WithType(IdentifierName("Guid")),
                Token(SyntaxKind.CommaToken)
            };
            var commandArgsAssignments = new List<StatementSyntax>()
            {
                ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("Id"), IdentifierName("id"))),
                ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("AggregateId"), IdentifierName("aggregateId")))
            };

            domainCommandArguments
                .OrderBy(x => x.AggregateProperty.Name)
                .ThenBy(x => x.AggregateProperty.Type)
                .ToList()
                .ForEach(x => 
                {
                    var propertyName = x.AggregateProperty.Name.First().ToString().ToLower() + x.AggregateProperty.Name.Substring(1);

                    commandArgs.Add(Parameter(Identifier(propertyName)).WithType(IdentifierName(x.AggregateProperty.Type)));
                    commandArgs.Add(Token(SyntaxKind.CommaToken));

                    commandArgsAssignments.Add(ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName(x.AggregateProperty.Name), IdentifierName(propertyName))));
                });

            commandArgs.Add(Parameter(Identifier("aggregateId")).WithType(IdentifierName("Guid")));
            
            return ConstructorDeclaration(Identifier(constructorIdentifier))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(commandArgs)))
                .WithBody(Block(commandArgsAssignments));
        }

        private static ClassDeclarationSyntax GetAggregateEventHandlerClass(Aggregate domainAggregate)
        {
            if (domainAggregate == null)
                throw new ArgumentNullException(nameof(domainAggregate));

            return ClassDeclaration($"{domainAggregate.Name}EventHandler")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(GetAggregateEventHandlerBaseList(domainAggregate.Events))
                .WithMembers(List(GetAggregateEventHandlerImplementations(domainAggregate.Events)));
        }

        private static ClassDeclarationSyntax GetAggregateCommandHandlerClass(Aggregate domainAggregate)
        {
            if (domainAggregate == null)
                throw new ArgumentNullException(nameof(domainAggregate));

            var baseList = new List<SyntaxNodeOrToken>()
            {
                SimpleBaseType(IdentifierName("AsdCommandHandler"))
            };
            var classMembers = new List<MemberDeclarationSyntax>();

            var privateReadOnlyIAggregateRepository = FieldDeclaration(
                VariableDeclaration(IdentifierName(
                    $"{domainAggregate.Name}Repository"))
                        .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("_repository")))))
                        .WithModifiers(TokenList(new[]
                        {
                            Token(SyntaxKind.PrivateKeyword),
                            Token(SyntaxKind.ReadOnlyKeyword)
                        }));

            var constructor = ConstructorDeclaration(Identifier($"{domainAggregate.Name}CommandHandler"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(new SyntaxNodeOrToken[]
                {
                    Parameter(Identifier("unitOfWork"))
                        .WithType(IdentifierName("IAsdUnitOfWork")),
                    Token(SyntaxKind.CommaToken),
                    Parameter(Identifier("bus"))
                        .WithType(IdentifierName("IAsdBus")),
                    Token(SyntaxKind.CommaToken),
                    Parameter(Identifier("notifications"))
                        .WithType(GenericName(Identifier("IAsdDomainNotificationHandler"))
                            .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName("AsdDomainNotification"))))),
                                Token(SyntaxKind.CommaToken),
                                    Parameter(Identifier("repository"))
                                        .WithType(IdentifierName($"{domainAggregate.Name}Repository"))
                })))
                .WithInitializer(ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                {
                    Argument(IdentifierName("unitOfWork")),
                    Token(SyntaxKind.CommaToken),
                    Argument(IdentifierName("bus")),
                    Token(SyntaxKind.CommaToken),
                    Argument(IdentifierName("notifications"))
                }))))
                .WithBody(Block(IfStatement(BinaryExpression(SyntaxKind.EqualsExpression,
                    IdentifierName("repository"),
                    LiteralExpression(SyntaxKind.NullLiteralExpression)),
                        ThrowStatement(ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                            .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("repository"))))))))))),
                    ExpressionStatement(AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, IdentifierName("_repository"), IdentifierName("repository")))));
            
            classMembers.Add(privateReadOnlyIAggregateRepository);
            classMembers.Add(constructor);
            domainAggregate.Commands?
                .Where(x => !x.Deleted)
                .ToList()
                .ForEach(x => 
                {
                    baseList.Add(Token(SyntaxKind.CommaToken));
                    baseList.Add(SimpleBaseType(GenericName(Identifier("IAsdHandler"))
                        .WithTypeArgumentList(TypeArgumentList(SingletonSeparatedList<TypeSyntax>(IdentifierName($"{x.CommandName}Command"))))));

                    var ifMessageIsNullStatement = IfStatement(BinaryExpression(
                        SyntaxKind.EqualsExpression, IdentifierName("message"), LiteralExpression(
                            SyntaxKind.NullLiteralExpression)), ThrowStatement(
                                ObjectCreationExpression(IdentifierName("ArgumentNullException"))
                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("message")))))))))));
                    var ifMessageIsNotValidBlock = Block(ExpressionStatement(InvocationExpression(IdentifierName("NotifyValidationErrors"))
                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("message")))))), ReturnStatement());
                    var ifMessageIsNotValidStatement = IfStatement(PrefixUnaryExpression(
                        SyntaxKind.LogicalNotExpression, InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, IdentifierName("message"), IdentifierName("IsValid")))), ifMessageIsNotValidBlock);

                    var bodyBlock = new List<StatementSyntax>()
                    {
                        ifMessageIsNullStatement,
                        ifMessageIsNotValidStatement,
                    };

                    switch (x.CommandType.ToLower())
                    {
                        case "create":
                            var assignmentExpressions = new List<SyntaxNodeOrToken>();
                            
                            x.DomainCommandArguments
                                .Where(y => !y.Deleted)
                                .ToList()
                                .ForEach(y => 
                            {
                                assignmentExpressions.Add(AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression, 
                                    IdentifierName(y.AggregateProperty.Name), 
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression, 
                                        IdentifierName("message"), 
                                        IdentifierName(y.AggregateProperty.Name))));
                                assignmentExpressions.Add(Token(SyntaxKind.CommaToken));
                            });
                            
                            bodyBlock.Add(LocalDeclarationStatement(VariableDeclaration(IdentifierName("var"))
                                .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("entity"))
                                    .WithInitializer(EqualsValueClause(ObjectCreationExpression(IdentifierName($"{domainAggregate.Name}Criteria"))
                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression, IdentifierName("message"), IdentifierName("Id"))))))
                                        .WithInitializer(InitializerExpression(
                                            SyntaxKind.ObjectInitializerExpression, SeparatedList<ExpressionSyntax>(assignmentExpressions)))))))));
                            bodyBlock.Add(ExpressionStatement(InvocationExpression(MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, IdentifierName("_repository"), IdentifierName("Add")))
                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("entity")))))));
                            break;
                        case "delete":
                            bodyBlock.Add(LocalDeclarationStatement(VariableDeclaration(IdentifierName("var"))
                                .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("entity"))
                                .WithInitializer(EqualsValueClause(InvocationExpression(MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_repository"),
                                    IdentifierName("GetById")))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("message"),
                                    IdentifierName("Id"))))))))))));

                            bodyBlock.Add(IfStatement(BinaryExpression(
                                SyntaxKind.EqualsExpression, 
                                IdentifierName("entity"), 
                                LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                ThrowStatement(ObjectCreationExpression(IdentifierName("NullReferenceException"))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("entity"))))))))))));

                            bodyBlock.Add(ExpressionStatement(InvocationExpression(MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, 
                                IdentifierName("_repository"), 
                                IdentifierName("Remove")))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("entity")))))));
                            break;
                        case "update":
                            bodyBlock.Add(LocalDeclarationStatement(VariableDeclaration(IdentifierName("var"))
                                .WithVariables(SingletonSeparatedList(VariableDeclarator(Identifier("entity"))
                                    .WithInitializer(EqualsValueClause(InvocationExpression(MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression, 
                                        IdentifierName("_repository"), 
                                        IdentifierName("GetById")))
                                            .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression, 
                                                IdentifierName("message"), 
                                                IdentifierName("Id"))))))))))));

                            bodyBlock.Add(IfStatement(BinaryExpression(
                                SyntaxKind.EqualsExpression, 
                                IdentifierName("entity"), 
                                LiteralExpression(SyntaxKind.NullLiteralExpression)),
                                    ThrowStatement(ObjectCreationExpression(IdentifierName("NullReferenceException"))
                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(InvocationExpression(IdentifierName("nameof"))
                                        .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("entity"))))))))))));

                            x.DomainCommandArguments
                                .Where(y => !y.Deleted)
                                .ToList()
                                .ForEach(y => 
                                {
                                    bodyBlock.Add(ExpressionStatement(AssignmentExpression(
                                        SyntaxKind.SimpleAssignmentExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression, IdentifierName("entity"), IdentifierName(y.AggregateProperty.Name)),
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression, IdentifierName("message"), IdentifierName(y.AggregateProperty.Name)))));
                                });

                            bodyBlock.Add(ExpressionStatement(InvocationExpression(MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("_repository"),
                                IdentifierName("Update")))
                                    .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("entity")))))));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                                        
                    bodyBlock.Add(IfStatement(InvocationExpression(IdentifierName("Commit")), ExpressionStatement(InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("Bus"), IdentifierName("RaiseEvent")))
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(ObjectCreationExpression(IdentifierName($"{x.Event.EventName}Event"))
                                .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                                {
                                    Argument(IdentifierName("entity")),
                                    Token(SyntaxKind.CommaToken),
                                    Argument(MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("message"),
                                        IdentifierName("AggregateId")))
                                }))))))))));
                    
                    var methodDeclaration = MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("Handle"))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("message")).WithType(IdentifierName($"{x.CommandName}Command")))))
                        .WithBody(Block(bodyBlock));
                    classMembers.Add(methodDeclaration);
                });
            
            return ClassDeclaration($"{domainAggregate.Name}CommandHandler")
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBaseList(BaseList(SeparatedList<BaseTypeSyntax>(baseList)))
                .WithMembers(List(classMembers));
        }

        private static IEnumerable<MemberDeclarationSyntax> GetAggregateEventHandlerImplementations(IEnumerable<Event> domainEvents)
        {
            if (domainEvents == null)
                throw new ArgumentNullException(nameof(domainEvents));

            return domainEvents.Select(x => 
            {
                return MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)), Identifier("Handle"))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(ParameterList(SingletonSeparatedList(Parameter(Identifier("message")).WithType(IdentifierName($"{x.EventName}Event")))))
                    .WithBody(Block());
            });
        }

        private static BaseListSyntax GetAggregateEventHandlerBaseList(IEnumerable<Event> domainEvents)
        {
            if (domainEvents == null)
                throw new ArgumentNullException(nameof(domainEvents));
            var items = new List<SyntaxNodeOrToken>();
            for(var i = 0; i < domainEvents.Count(); ++i)
            {
                var eventIdentifierName = IdentifierName($"{domainEvents.ElementAt(i).EventName}Event");
                var eventSingletonSeparatedList = SingletonSeparatedList<TypeSyntax>(eventIdentifierName);
                var eventGenericName = GenericName(Identifier("IAsdHandler"))
                    .WithTypeArgumentList(TypeArgumentList(eventSingletonSeparatedList));

                items.Add(SimpleBaseType(eventGenericName));
                if ((i + 1) != domainEvents.Count())
                    items.Add(Token(SyntaxKind.CommaToken));
            }
            return BaseList(SeparatedList<BaseTypeSyntax>(items));
        }

        private static BaseListSyntax GetDomainEventBaseList()
        {
            return BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(IdentifierName(nameof(AsdEvent)))));
        }

        private static ConstructorDeclarationSyntax GetDomainEventConstructor(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                throw new ArgumentNullException(nameof(eventName));

            return ConstructorDeclaration(Identifier($"{eventName}Event"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(new SyntaxNodeOrToken[]
                {
                    Parameter(Identifier("entity")).WithType(IdentifierName("Event")),
                    Token(SyntaxKind.CommaToken),
                    Parameter(Identifier("aggregateId")).WithType(IdentifierName("Guid"))
                })))
                .WithInitializer(ConstructorInitializer(SyntaxKind.BaseConstructorInitializer, ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                {
                    Argument(IdentifierName("entity")),
                    Token(SyntaxKind.CommaToken),
                    Argument(IdentifierName("aggregateId"))
                }))))
                .WithBody(Block());
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