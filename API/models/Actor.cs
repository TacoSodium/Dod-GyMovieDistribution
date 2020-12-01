using System;

namespace API.models
{
    public class Actor
    {
        public int ActorNo { get; set; }
        public string FullName { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }

        public Actor(int actorNo, string givenname, string surname)
        {
            this.ActorNo = actorNo;
            this.Givenname = givenname;
            this.Surname = surname;
            SetFullName();
        }

        public void SetFullName()
        {
            this.FullName = this.Givenname + " " + this.Surname;
        }
    }
}
