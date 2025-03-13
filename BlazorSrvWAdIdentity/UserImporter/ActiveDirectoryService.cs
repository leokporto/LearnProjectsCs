using System.DirectoryServices.AccountManagement;

namespace UserImporter
{
    public class ActiveDirectoryService
    {
        private readonly string _domainName;

        public ActiveDirectoryService(string domainName)
        {
            _domainName = domainName;
        }

        public List<SpinTrackUser> GetActiveUsers()
        {
            var users = new List<SpinTrackUser>();

            using (var context = new PrincipalContext(ContextType.Domain, _domainName))
            using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
            {
                if(searcher == null) 
                    return users;
                
                var allUsers = searcher.FindAll();

                foreach (var result in allUsers)
                {
                    var user = result as UserPrincipal;
                    if (user != null && user.Enabled == true)
                    {
                        if(user.Enabled == false)
                            continue;

                        if (string.Equals(user.SamAccountName,"Administrador", StringComparison.CurrentCultureIgnoreCase) || 
                            string.Equals(user.SamAccountName, "Administrator", StringComparison.CurrentCultureIgnoreCase)) 
                            continue;

                        //TODO: Fazer a atribuicao do role aos usuarios
                        string role = "Usuario";

                        if(string.Equals(user.SamAccountName, "leonardo.porto", StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(user.SamAccountName, "guilherme.tavares", StringComparison.CurrentCultureIgnoreCase))
                            role = "Administrador";

                        if (string.Equals(user.SamAccountName, "erica.lira", StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(user.SamAccountName, "erika.gomes", StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(user.SamAccountName, "danielle.ribeiro", StringComparison.CurrentCultureIgnoreCase))
                            role = "Licenciador";

                        users.Add(new SpinTrackUser()
                        { 
                            Username = user.SamAccountName,
                            Email = user.EmailAddress,
                            Role = role
                        }); // Retorna "usuario", sem "DOMINIO\"
                    }
                }
            }

            return users;
        }
    }

    public class SpinTrackUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}




