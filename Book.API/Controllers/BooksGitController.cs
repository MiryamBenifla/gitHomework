using Book.BL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksGitController : ControllerBase
    {
        private IBooksGitService _booksGitService;
        private IMapper _mapper;

        public BooksGitController(IBooksGitService usersService, IMapper mapper)
        {
            _booksGitService = booksGitService;
            _mapper = mapper;
        }

        [HttpGet]
        public List<UserDTO> GetUsers()
        {
            return _usersService.GetUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUserById(Guid userId)
        {
            return Ok(_usersService.GetUserById(userId));
        }

        // הוספת משתמש חדש
        [HttpPost]
        public async Task<Guid> CreateUser([FromBody] UserRequest user)
        {
            try
            {
                // מיפוי ה-UserRequest ל-UserDTO וקריאה לשירות ליצירת משתמש חדש
                return await _usersService.CreateUser(_mapper.Map<UserDTO>(user));
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה והחזרת שגיאה למשתמש
                throw new Exception("Failed to create user", ex);
            }
        }


        // עדכון משתמש
        [HttpPut("{userId}")]
        public async Task<UserDTO> UpdateUser(Guid userId, [FromBody] UserRequest user)
        {
            try
            {
                // מיפוי ה-UserRequest ל-UserDTO והגדרת ה-userId
                UserDTO newUser = _mapper.Map<UserDTO>(user);
                newUser.UserId = userId;

                // קריאה לשירות לעדכון המשתמש והחזרת התוצאה
                UserDTO updatedUser = await _usersService.UpdateUser(newUser);
                return updatedUser;
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה והשלכת שגיאה מותאמת
                throw new Exception("Failed to update user", ex);
            }
        }
    }
}
