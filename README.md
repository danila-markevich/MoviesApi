# MoviesApi

REST API для фильмотеки на ASP.NET Core Web API.

## Стек

- C# / .NET 10
- ASP.NET Core Web API — контроллеры, DI
- Entity Framework Core — ORM, миграции
- SQL Server — база данных

## Функциональность

- CRUD для фильмов и жанров
- Связь один-ко-многим (Movie → Genre)
- DTO для запросов и ответов
- Валидация входных данных (Data Annotations)
- Swagger UI для тестирования

## Эндпоинты

### Movies

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/movies | Все фильмы |
| GET | /api/movies/{id} | Фильм по Id |
| POST | /api/movies | Создать фильм |
| PUT | /api/movies/{id} | Обновить фильм |
| DELETE | /api/movies/{id} | Удалить фильм |

### Genres

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/genres | Все жанры с количеством фильмов |
| GET | /api/genres/{id} | Жанр по Id |
| POST | /api/genres | Создать жанр |
| PUT | /api/genres/{id} | Обновить жанр |
| DELETE | /api/genres/{id} | Удалить жанр |

## Архитектура

- Models — сущности БД (Movie, Genre)
- DTOs — контракты API (MovieDto, CreateMovieDto, UpdateMovieDto)
- Data — DbContext
- Controllers — обработка HTTP-запросов

## Как запустить

1. Установить .NET 10 SDK и SQL Server Express.
2. Клонировать репозиторий: git clone https://github.com/danila-markevich/MoviesApi.git
3. Настроить строку подключения в Program.cs.
4. Применить миграции: dotnet ef database update
5. Запустить: dotnet run
6. Открыть Swagger: https://localhost:XXXX/swagger

## Автор

Danila Markevich — https://github.com/danila-markevich