namespace InstaTax.Core.DomainObjects{
    public class User{

        public IRepository repository;
        public string Id { get; set; }
        public Password Password{ get; set; }
        
        public User(string id, Password password, IRepository repository){
            this.repository = repository;
            Id = id;
            Password = password;
        }

        public void Save(){
            if (repository.CheckIfUnique(this)){
                repository.Save(this);
            }
            else{
                throw new DuplicateUserException("Unique user not found");
            }
        }
    }
}