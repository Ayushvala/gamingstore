using LearnDapper.Models;

namespace LearnDapper.Interface
{
    public interface IDapperService
    {
        Task<List<ToDo>> GetAll();
        Task<ToDo>GetTaskById(int id);

        Task<String> CreateTask(ToDo toDo);

        Task<String> UpdateTask(ToDo toDo);
        Task<String> DeleteTask(int id);

    }
}
