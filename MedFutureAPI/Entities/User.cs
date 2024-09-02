using MedFuture.Core.Entities;

namespace MedFutureAPI.Entities
{
    public class User : BaseEntity
    {

        public string Name { get; set; }
        public string NickName { get; set; }
        public DateTime Birth { get; set; }
        public List<string> Stack { get; set; }

        public void Update(string name, string nickname, DateTime birth, List<string> stack)
        {
            Name = name;
            NickName = nickname;
            Birth = birth;
            Stack = stack;
        }
    }
}
