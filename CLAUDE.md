\# BudgetApp — CLAUDE.md



\## Vad är projektet?

En webbaserad budgetapplikation för studenter. Användaren loggar manuellt transaktioner och fördelar dem på kategorier. Appen ersätter Excel-budgeten med ett enklare och smidigare flöde.



\## Tech Stack

\- Backend: .NET 10, Clean Architecture, MediatR, CQRS, EF Core, SQL, AutoMapper, FluentValidation, JWT, RBAC

\- Frontend: React (Vite), Tailwind CSS, Axios, React Router

\- Dokumentation: Swagger, Postman

\- Deploy: Frontend → Vercel, Backend → Railway/Render



\## Modeller \& Relationer

\- User (Id, Email, PasswordHash, Role, CreatedAt)

\- Budget (Id, UserId, Name, Month, Year, TotalAmount, CreatedAt)

\- Category (Id, BudgetId, Name, AllocatedAmount, CurrentBalance, CreatedAt)

\- Transaction (Id, CategoryId, Amount, Type, Description, Date)



Relationer:

\- User 1 → 1 Budget

\- Budget 1 → N Category

\- Category 1 → N Transaction



\## Viktiga beslut

\- Budget har Month och Year för att skilja på samma månad olika år

\- Transaction.Type är "expense" eller "income" — alltid positivt belopp

\- Category.CurrentBalance uppdateras automatiskt i CreateTransactionCommandHandler

\- Role är "Admin" (ser alla budgetar) eller "User" (ser bara sin egen)



\## Clean Architecture-lager

\- BudgetApp.API — Controllers, Program.cs, DependencyInjection.cs

\- BudgetApp.Application — Commands, Queries, Handlers, DTOs, Validators

\- BudgetApp.Domain — Entities (User, Budget, Category, Transaction)

\- BudgetApp.Infrastructure — Repositories, AppDbContext, Migrations



\## Konventioner

\- Klasser: PascalCase

\- Variabler: camelCase

\- Branch-namn: feature/add-category-crud

\- Commit-meddelanden: "Add CreateCategoryCommandHandler"

\- Repositories: IRepository<T> som bas (Generic Repository)

\- Alla service-registreringar i DependencyInjection.cs, inte Program.cs



\## GitHub

\- Backend repo: pauline8712/group-project-backend

\- Frontend repo: pauline8712/group-project-frontend

\- Project Board: GroupProject (döps om till BudgetApp)

\- Branch protection på main i båda repos

