namespace TAS.Infrastructure.Enums
{
    public class SystemEnum
    {
        public enum UserStatus
        {
            Activated = 1,
            Blocked = 2
        }

        public enum UserRoles
        {
            Admin = 1,
            Enterprise = 2,
            //Teacher = 3,
            Student = 4
        }
        public enum CourseStatus
        {
            Approved = 1,
            Pending = 2,
            Rejected = 3,
            Draft = 4
        }
    }
}
