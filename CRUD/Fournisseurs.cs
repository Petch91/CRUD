using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public struct Fournisseurs
    {
        public string Name,TvaNumber;
        public FournisseurType Type;
        public Personne Contact = new Personne();

        public Fournisseurs(string name, string tvaN, FournisseurType type, Personne contact)
        {
            Name = name;
            Type = type;
            Contact = contact;
            TvaNumber = tvaN;
        }
        public override string ToString()
        {
            return $"---------------------------\n" +
                   $"Nom     : {Name}\n" +
                   $"TVA     : {TvaNumber}\n" +
                   $"Type    : {Type}\n" +
                   $"Contact : {Contact.Name}\n" +
                   $"Numéro  : {Contact.PhoneNumber}\n" +
                   $"---------------------------";
        }
    }

    public class Personne
    {
        public string Name;
        public string PhoneNumber;

        public Personne(string name = "", string phoneNumber = "")
        {
            Name = name;
            PhoneNumber = phoneNumber; 
        }   

    }
}
