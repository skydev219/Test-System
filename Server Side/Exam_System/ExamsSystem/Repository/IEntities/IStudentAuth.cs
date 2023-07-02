namespace ExamsSystem.Repository.IEntities
{
    public interface IStudentAuth<Student> : IAuthentication<Student>
    {
        public Task Register(Student student);
        public Task<bool> IsUsernameTakenAsync(string username);
    }

}
