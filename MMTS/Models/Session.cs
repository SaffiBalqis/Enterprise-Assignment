namespace MMTS.Models
{
    public class Session
    {
        public int Id { get; set; }

        public int MentorId { get; set; }
        public Mentor? Mentor { get; set; }

        public int MenteeId { get; set; }
        public Mentee? Mentee { get; set; }

        public DateTime ScheduledTime { get; set; }
    }

}
