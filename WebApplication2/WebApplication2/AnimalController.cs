namespace WebApplication2;

using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
    {
        var columns = dt.Columns.Cast<DataColumn>();
        return dt.Rows.Cast<DataRow>()
            .Select(row => columns.ToDictionary(column => column.ColumnName, column => row[column])).ToList();
    }

    // GET: api/animals
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "name")
    {
        string query = $"SELECT * FROM Animals ORDER BY {orderBy}";
        DataTable table = new DataTable();
        using (SqlConnection myCon = new SqlConnection("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;"))
        // using (SqlConnection myCon = new SqlConnection("Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;User Id=s28320;Password=password;"))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    table.Load(myReader);
                }
            }
            myCon.Close();
        }

        var list = ConvertDataTableToList(table);
        return Ok(list);
    }

    // POST: api/animals
    [HttpPost]
    public IActionResult PostAnimal(Animal animal)
    {
        string query = $"INSERT INTO Animals (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";
        DataTable table = new DataTable();
        SqlDataReader myReader;
        using (SqlConnection myCon = new SqlConnection("Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;User Id=s28320;Password=password;"))
        {
            myCon.Open();
            using (SqlCommand myCommand = new SqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@Name", animal.Name);
                myCommand.Parameters.AddWithValue("@Description", animal.Description);
                myCommand.Parameters.AddWithValue("@Category", animal.Category);
                myCommand.Parameters.AddWithValue("@Area", animal.Area);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }
        }
        return new JsonResult("Added Successfully");
    }

    [HttpPut]
    public IActionResult UpdateAnimal(int id, string whatUpdate, string newData)
    {
        using (SqlConnection myCon = new SqlConnection("Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;User Id=s28320;Password=password;"))
        {
            SqlCommand myCom = new SqlCommand();
            myCom.Connection = myCon;
            myCom.CommandText = "UPDATE ANIMAL SET @whatUpdate = '@newData' WHERE @IdAnimal";
            myCom.Parameters.AddWithValue("@IdAnimal", id);
            myCom.Parameters.AddWithValue("@whatUpdate", whatUpdate.ToUpper());
            myCom.Parameters.AddWithValue("@newData", newData);

            myCon.Open();
            myCom.ExecuteNonQuery();

            myCon.Dispose();
            myCom.Dispose();

        }

        return new JsonResult("Updated Successfully");
    }


    [HttpDelete]
    public IActionResult DeleteAnimal(int id)
    {
        using (SqlConnection myCon = new SqlConnection("Server=db-mssql.pjwstk.edu.pl;Database=2019SBD;User Id=s28320;Password=password;"))
        {
            SqlCommand myCom = new SqlCommand();
            myCom.Connection = myCon;
            myCom.CommandText = "DELETE * FROM ANIMAL WHERE IDANIMAL = @IdAnimal";
            myCom.Parameters.AddWithValue("@IdAnimal", id);

            myCon.Open();
            myCom.ExecuteNonQuery();

            myCon.Dispose();
            myCom.Dispose();

        }
        return Ok();
    }
}