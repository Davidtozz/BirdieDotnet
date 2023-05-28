using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BirdieDotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ConversationsController : Controller
    {
        
        public readonly MySqlConnection _connection;
        //public readonly ConversationService _conversationService;

        public ConversationsController(MySqlConnection connection/*, ConversationService conversationService*/)
        {
            _connection = connection;
            //_conversationService = conversationService;
        }

        // Conversation-related routes
        [HttpGet] //! /api/conversations
        public IActionResult GetConversations()
        {
            using MySqlConnection Connection = _connection;

            try
            {
                Connection.Open();
            }
            catch (MySqlException)
            {
                return StatusCode(500, "Internal server error");
            }

            using MySqlCommand Command = Connection.CreateCommand();
            StringBuilder ConversationsList = new();

            //! selects all conversations and their respective participants' id
            Command.CommandText = "SELECT c.id AS conversation_id, c.name AS conversation_name, GROUP_CONCAT(p.user_id ORDER BY p.user_id) AS participant_ids, c.created_at FROM conversations AS c JOIN Participants AS p ON c.id = p.conversation_id  GROUP BY c.id;";
            var reader = Command.ExecuteReader();

            while (reader.Read())
            {
                var conversation = new Conversation
                {
                    Id = reader.GetInt32("conversation_id"),
                    Name = reader.GetString("conversation_name"),
                    CreatedAt = reader.GetDateTime("created_at"),
                    ParticipantsIds = reader.GetString("participant_ids")
                                            .Split(',')
                                            .Select(int.Parse)
                                            .ToList()
                };

                ConversationsList.Append($"{JsonConvert.SerializeObject(conversation)},\n");
            }

            return Ok(ConversationsList.ToString());
        }

        [HttpPost]
        [Route("conversations")]
        public IActionResult CreateConversation([FromBody] Conversation conversation)
        {


            return Ok();
            // Implementation
        }


        /*[HttpGet]
        [Route("conversations/{conversationId}")]
        public IActionResult GetConversationById(int conversationId)
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
