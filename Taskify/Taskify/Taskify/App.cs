using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Pages;
using Xamarin.Forms;
using Taskify.Users;
using Microsoft.WindowsAzure.MobileServices;


namespace Taskify
{
    public class App : Application
    {

        public static MobileServiceClient MobileService =
            new MobileServiceClient(
            "https://taskify.azurewebsites.net"
        );
        private List<User> regUsers;

        public App()
        {
            
            loadMock1();
            
            MainPage = new NavigationPage(new Login(regUsers));

        }
        public void loadMock1()
        {
            regUsers = new List<User>();
            User charlie = new User("Charlie", 2332);
            regUsers.Add(charlie);
            User mateo = new User("Mateo", 1234);
            regUsers.Add(mateo);
            User facundo = new User("FacuPiola", 4321);
            regUsers.Add(facundo);
            User guillermo = new User("GuilleSape", 9876);
            regUsers.Add(guillermo);
            User anabel = new User("Anabel", 1111);
            regUsers.Add(anabel);
            User dem = new User("Demetrio", 2222);
            regUsers.Add(dem);
            User se = new User("Sebita", 3333);
            regUsers.Add(se);

            charlie.acceptRequest(mateo);
            charlie.acceptRequest(guillermo);
            mateo.acceptRequest(facundo);
            mateo.acceptRequest(guillermo);
            facundo.acceptRequest(guillermo);

            charlie.sendRequest(facundo);
            facundo.sendRequest(charlie);
            charlie.sendRequest(mateo);
            anabel.sendRequest(charlie);
            charlie.sendRequest(dem);

            facundo.getTasks().Add(new Item.Item("F1"));
            mateo.getTasks().Add(new Item.Item("M1"));
            mateo.getTasks().Add(new Item.Item("M2"));
            charlie.getTasks().Add(new Item.Item("C1"){state = "En Proceso"});
            charlie.getTasks().Add(new Item.Item("C2"){state = "Terminada"});
            charlie.getTasks().Add(new Item.Item("C3"));
            charlie.getTasks().Add(new Item.Item("C4"));
            charlie.getTasks().Add(new Item.Item("C5"));
            charlie.getTasks().Add(new Item.Item("C6"));
            charlie.getTasks().Add(new Item.Item("C7"));
            charlie.getTasks().Add(new Item.Item("C8"));
            charlie.getTasks().Add(new Item.Item("C9"));

        }
        
    };
}
