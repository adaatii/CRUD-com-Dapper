using MySqlConnector;
using Dapper;
using CrudDapper.Models;


namespace CrudDapper.Services.LivroService

{
    public class LivroService : ILivroInterface
    {
        private readonly IConfiguration _configuration;
        private readonly string getConnection;

        public LivroService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            getConnection = _configuration.GetConnectionString("DefaultConnection")
                             ?? throw new InvalidOperationException("A string de conexão 'DefaultConnection' não foi encontrada.");
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using (var con = new MySqlConnection(getConnection))
            {
                var sql = "SELECT * FROM Livros";
                return await con.QueryAsync<Livro>(sql);
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using (var con = new MySqlConnection(getConnection))
            {
                var sql = "SELECT * FROM Livros WHERE Id = @Id";
                var livro = await con.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = livroId });
                return livro;
            }
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (var con = new MySqlConnection(getConnection))
            {
                var sql = "INSERT INTO Livros (Titulo, Autor) VALUES (@Titulo, @Autor)";
                await con.ExecuteAsync(sql, livro);
                return await GetAllLivros();
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using (var con = new MySqlConnection(getConnection))
            {
                var sql = "UPDATE Livros SET Titulo = @Titulo, Autor = @Autor WHERE Id = @Id";
                await con.ExecuteAsync(sql, livro);
                return await GetAllLivros();
            }
        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int id)
        {
            using (var con = new MySqlConnection(getConnection))
            {
                var sql = "DELETE FROM Livros WHERE Id = @Id";
                await con.ExecuteAsync(sql, new { Id = id });
                return await GetAllLivros();
            }
        }
    }
}
