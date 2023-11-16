using AutoMapper;
using AutoMapperSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace AutoMapperSample.Endpoints
{
    public static class TodoItemsEndpoints
    {
        public static void RegisterTodoItemsEndpoints(this WebApplication app)
        {
            RouteGroupBuilder todoItems = app.MapGroup("/todoitems")
                .WithOpenApi()
                .WithTags("代辦事項");

            todoItems.MapGet("/", GetAllTodos);
                //.Produces<Todo>(StatusCodes.Status200OK)
                //.Produces(StatusCodes.Status404NotFound);

            todoItems.MapGet("/complete", GetCompleteTodos);
            todoItems.MapGet("/{id}", GetTodo);
            todoItems.MapPost("/", CreateTodo);
            todoItems.MapPut("/{id}", UpdateTodo);
            todoItems.MapDelete("/{id}", DeleteTodo);
        }

        static async Task<IResult> GetAllTodos(TodoDbContext db, IMapper mapper)
        {
            //old
           //return TypedResults.Ok(await db.Todos.Select(x => new TodoItemDTO() { 
           //     Id = x.Id,
           //     Name = x.Name,
           //     IsComplete = x.IsComplete,
           //     OverdueTime = x.OverdueTime
           //}).ToArrayAsync());

            //new
;           var result = await db.Todos.ToArrayAsync();
            return TypedResults.Ok(mapper.Map<TodoItemDTO[]>(result));
        }

        static async Task<IResult> GetCompleteTodos(TodoDbContext db, IMapper mapper)
        {
            //old
            //return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).Select(x => new TodoItemDTO()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    IsComplete = x.IsComplete,
            //    OverdueTime = x.OverdueTime
            //}).ToListAsync());

            //new
            var result = await db.Todos.Where(t => t.IsComplete).ToListAsync();
            return TypedResults.Ok(mapper.Map<List<TodoItemDTO>>(result));
        }

        static async Task<IResult> GetTodo(int id, TodoDbContext db, IMapper mapper)
        {
            //old
            return await db.Todos.FindAsync(id)
                is Todo todo
                    ? TypedResults.Ok(new TodoItemDTO()
                    {
                        Id = todo.Id,
                        Name = todo.Name,
                        IsComplete = todo.IsComplete
                    })
                    : TypedResults.NotFound();

            ////new
            //return await db.Todos.FindAsync(id)
            //    is Todo todo
            //        ? TypedResults.Ok(mapper.Map<TodoItemDTO>(todo))
            //        : TypedResults.NotFound();
        }

        static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDbContext db, IMapper mapper)
        {
            var todoItem = new Todo
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            db.Todos.Add(todoItem);
            await db.SaveChangesAsync();

            //old
            //todoItemDTO = new TodoItemDTO()
            //{
            //    Id = todoItem.Id,
            //    Name = todoItem.Name,
            //    IsComplete = todoItem.IsComplete,
            //    OverdueTime = todoItem.OverdueTime
            //};

            //new 
            todoItemDTO = mapper.Map<TodoItemDTO>(todoItem);

            return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
        }

        static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, TodoDbContext db, IMapper mapper)
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return TypedResults.NotFound();

            todo.Name = todoItemDTO.Name;
            todo.IsComplete = todoItemDTO.IsComplete;

            //todo = mapper.Map(todoItemDTO, todo);

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        static async Task<IResult> DeleteTodo(int id, TodoDbContext db)
        {
            if (await db.Todos.FindAsync(id) is Todo todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
