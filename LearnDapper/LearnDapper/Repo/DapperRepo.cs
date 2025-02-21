using Dapper;
using LearnDapper.DapperContext;
using LearnDapper.Interface;
using LearnDapper.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LearnDapper.Repo
{
    public class DapperRepo : IDapperService
    {
        private readonly DapperDbContext _dapperContext;

        public DapperRepo(DapperDbContext dapperContext)
        {

            _dapperContext = dapperContext;
        }

        public async Task<string> CreateTask(ToDo toDo)
        {
            var sql = "INSERT INTO todos(Name, Description, Status,CreatedAt) VALUES(@Name, @Description, @Status,@CreatedAt)";

            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("Name", toDo.Name, System.Data.DbType.String);
            parameters.Add("Description", toDo.Description, System.Data.DbType.String);
            parameters.Add("Status", toDo.Status, System.Data.DbType.String);
            parameters.Add("CreatedAt", toDo.CreatedAt, System.Data.DbType.String);


            using (var connection = _dapperContext.CreateConnection())
            {
               
                await connection.ExecuteAsync(sql, parameters);

              
                response = "pass";
            }

          
            return response;
        }


        public async Task<string> DeleteTask(int id)
        {
            string response = string.Empty;
            var sql = "Delete from todos where id=@id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = await connection.ExecuteAsync(sql,new { id});
                response = "pass";
            }
            return response;

        }

        public async Task<List<ToDo>> GetAll()
        {
            var sql = "select * from todos";
            using (var connection = _dapperContext.CreateConnection())
            {
                var tasks = await connection.QueryAsync<ToDo>(sql);
                return tasks.ToList();
            }
        }

        public async Task<ToDo> GetTaskById(int id)
        {
            var sql = "select * from todos where id=@id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var  task= await connection.QueryFirstOrDefaultAsync<ToDo>(sql,new { id });
                return task;
            }
        }

        public  async Task<string> UpdateTask(ToDo toDo)
        {
            var sql = "update todos set Name=@Name, Description=@Description, Status=@Status,CreatedAt= @CreatedAt where Id=@Id";

            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("Id", toDo.Id);
            parameters.Add("Name", toDo.Name, System.Data.DbType.String);
            parameters.Add("Description", toDo.Description, System.Data.DbType.String);
            parameters.Add("Status", toDo.Status, System.Data.DbType.String);
            parameters.Add("CreatedAt", toDo.CreatedAt, System.Data.DbType.DateTime);

            using (var connection = _dapperContext.CreateConnection())
            {

                await connection.ExecuteAsync(sql, parameters);


                response = "pass";
            }


            return response;
        }
    }
}
