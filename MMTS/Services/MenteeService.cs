using MMTS.Models;
namespace MMTS.Services
{
    public class MenteeService : IMenteeService
    {
        public string GetMenteeInfo(int id)
        {
            return $"Mentee #{id}: John Doe";
        }
    }
}
