using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BirdieDotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        
        public readonly MySqlConnection _connection;

        public ChatController(MySqlConnection connection)
        {
            _connection = connection;

        }

        // Conversation-related routes
        [HttpGet]
        [Route("conversations")]
        public IActionResult GetConversations()
        {
            using MySqlConnection Connection = _connection;

            try 
            {
                Connection.Open();
            }
            catch(MySqlException)
            {
                return StatusCode(500, "Internal server error");
            }

            using MySqlCommand Command = Connection.CreateCommand();
            List<Conversation> ConversationsList = new();

            //! selects all conversations and their respective participant's id
            Command.CommandText = "SELECT c.id AS conversation_id, c.name AS conversation_name, GROUP_CONCAT(p.user_id ORDER BY p.user_id) AS participant_ids, c.created_at FROM Conversations c JOIN Participants p ON c.id = p.conversation_id GROUP BY c.id;";
            var reader = Command.ExecuteReader();

            if (reader.Read())
            {
                string ParticipantsIds = reader.GetString("participant_ids");

                var conversation = new Conversation
                {
                    Id = reader.GetInt32("conversation_id"),
                    Name = reader.GetString("conversation_name"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    ParticipantsIds = ParticipantsIds.Split(',')
                                                     .Select(int.Parse)
                                                     .ToList()
                    
                };

                ConversationsList.Add(conversation);
            }

            var SerializedConversationsList = JsonConvert.SerializeObject(ConversationsList);

            return Ok(SerializedConversationsList);
        }

        /*[HttpGet]
        [Route("conversations/{conversationId}")]
        public IActionResult GetConversationById(int conversationId)
        {
            // Implementation
        }

        [HttpPost]
        [Route("conversations")]
        public IActionResult CreateConversation([FromBody] Conversation conversation)
        {
            // Implementation
        }

        // Message-related routes
        [HttpGet]
        [Route("conversations/{conversationId}/messages")]
        public IActionResult GetMessages(int conversationId)
        {
            // Implementation
        }

        [HttpGet]
        [Route("conversations/{conversationId}/messages/{messageId}")]
        public IActionResult GetMessage(int conversationId, int messageId)
        {
            // Implementation
        }

        [HttpPost]
        [Route("conversations/{conversationId}/messages")]
        public IActionResult SendMessage(int conversationId, [FromBody] Message message)
        {
            // Implementation
        }

        // Participant-related routes
        [HttpPost]
        [Route("conversations/{conversationId}/participants")]
        public IActionResult AddParticipant(int conversationId, [FromBody] User user)
        {
            // Implementation
        }

        [HttpDelete]
        [Route("conversations/{conversationId}/participants/{participantId}")]
        public IActionResult RemoveParticipant(int conversationId, int participantId)
        {
            // Implementation
        }*/
    }

}
