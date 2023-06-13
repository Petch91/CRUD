using CRUD;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

int choix = 0;
List<Fournisseurs> annuaire = new List<Fournisseurs>();
bool vide = EmptyList(annuaire);
do
{
    vide = EmptyList(annuaire);
    choix = Menu(vide);
    switch (choix)
    {
        case 1:
            {
                Add(annuaire);
                break;
            }
        case 2:
            {
                if (!vide) ModifyFournisseur(annuaire);
                                         
                break;  
            }
        case 3:
            {
                DeleteFournisseur(annuaire);
                break;
            }
        case 4:
            {
                ShowFournisseur(annuaire);
                break;
            }
        case 5:
            {
                SortFournisseurs(annuaire);
                break;
            }

    }    
}while((choix != 6 && !vide) || (choix != 2 && vide));



int Menu(bool vide)
{

    int choix;
    string s;
    do
    {
        Console.Clear();
        if (vide)
        {
            Console.WriteLine("  MENU : Fournisseurs\n" +
                              "---------------------\n" +
                              "1: Ajouter\n" +
                              "2: Quitter");
        }
        else
        {
            Console.WriteLine("  MENU : Fournisseurs\n" +
                              "---------------------\n" +
                              "1: Ajouter\n" +
                              "2: Modifier\n" +
                              "3: Supprimer\n" +
                              "4: Afficher\n" +
                              "5: Trier\n" +
                              "6: Quitter"
                              );
        }
        Console.WriteLine("Quel est votre choix? ");
        s = Console.ReadLine();

    } while (!(int.TryParse(s, out choix)) && ((choix != 6 && !vide) || (choix != 2 && vide)));


    return choix;
}

bool EmptyList(List<Fournisseurs> list)
{
    return list.Count == 0;
}

int SelectFournisseur(List<Fournisseurs> list)
{
    int choix;
    string s;
    while (true)
    {
        do
        {
            ShowList(list);
            Console.WriteLine("Quel fournisseur voulez vous choisir? ");
            s = Console.ReadLine();
        } while (!int.TryParse(s, out choix));
        choix -= 1;
        if (choix >= list.Count)  return list.Count;
        break;
    }
    return choix;

}
void ShowList(List<Fournisseurs> list)
{
    Console.Clear();
    StringBuilder sb = new StringBuilder();
    foreach(Fournisseurs fo in list)
    {
        sb.AppendLine($"{list.IndexOf(fo)+1}: {fo.Name}");
    }
    sb.AppendLine($"{(list.Count+1)}: Retour");
    Console.WriteLine(sb);
}

void Add(List<Fournisseurs> list)
{
    Console.Clear() ;
    string name = "",tva = "";
    Personne contact = new Personne();
    FournisseurType type  = FournisseurType.Aucun;
    Console.WriteLine("Quel est le nom du fournisseur? ");
    name= Console.ReadLine();
    Console.WriteLine("Quel est le numéro TVA du fournisseur? ");
    tva = Console.ReadLine();
    Console.WriteLine("Quel est le type du fournisseur? ");
    ShowType();
    type = (FournisseurType) (int.Parse(Console.ReadLine())-1);
    Console.WriteLine("Quel est le nom du contact? ");
    contact.Name = Console.ReadLine();
    Console.WriteLine("Quel est le numéro du contact? ");
    contact.PhoneNumber = Console.ReadLine();
    list.Add(new Fournisseurs(name,tva,type,contact));
}

void ShowType()
{
    StringBuilder sb = new StringBuilder();
    foreach (FournisseurType p in Enum.GetValues<FournisseurType>() )
    {
        sb.AppendLine((int)p < 9 ? $"{(int)p + 1}  : {p}" : $"{(int)p + 1} : {p}");
    }
    Console.WriteLine(sb);
}

void ShowFournisseur(List<Fournisseurs> list)
{
    
    Console.Clear();
    int choix = SelectFournisseur(list);
    if (choix >= list.Count) return;
    Console.Clear();
    Console.WriteLine(list.ElementAt(choix));
    Console.ReadKey();
}

void DeleteFournisseur(List<Fournisseurs>  list)
{
    Console.Clear();
    int choix = SelectFournisseur(list);
    if (choix >= list.Count) return;
    list.RemoveAt(choix);
}

void ModifyFournisseur(List<Fournisseurs> list)
{
    int c = 0;
    int choix;
    do
    {
        Console.Clear();
        choix = SelectFournisseur(list);
        if (choix >= list.Count) return;
        Fournisseurs fo = list.ElementAt(choix);
        list.RemoveAt(choix);
        do
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Que voulez vous modifier?\n" +
                          "-------------------------\n" +
                          "1: Le nom \n" +
                          "2: Le type \n" +
                          "3: Le numéro de tva\n" +
                          "4: Le nom du contact \n" +
                          "5: Le numéro téléphone \n" +
                          "6: Revenir en arrière\n" +
                          "7: Menu Principal \n");
            Console.WriteLine(sb);
            c = int.Parse(Console.ReadLine());
            switch (c)
            {
                case 1:
                    {
                        Console.WriteLine("Quel est le nouveau nom?");
                        fo.Name = Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Quel est le nouveau type?");
                        ShowType();
                        fo.Type = (FournisseurType)(int.Parse(Console.ReadLine()) - 1);
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Quel est le nouveau numéro de tva?");
                        fo.TvaNumber = Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Quel est le nouveau nom  du contact?");
                        fo.Contact.Name = Console.ReadLine();
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Quel est le nouveau téléphone du contact?");
                        fo.Contact.PhoneNumber = Console.ReadLine();
                        break;
                    }
                case 6:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }while (c != 7 && c != 6);
        list.Insert(choix, fo);
    } while (c != 7);

}

void SortFournisseurs(List<Fournisseurs> list)
{
    List<Fournisseurs> cList = list;
    for (int i = 0; i < cList.Count - 1; i++)
    {
        for (int j = 0; j<cList.Count - 1-i; j++)
        {
            if (char.Parse(cList[j].Name.Substring(0,1)) > (char.Parse(cList[j+1].Name.Substring(0, 1))))
            {
                (cList[j], cList[j + 1]) = (cList[j + 1], cList[j]);
            }
        }
    }
    Console.WriteLine("Voici les fournisseurs triès par ordre alphabétique: ");
    ShowList(cList);
    while (true)
    {
        Console.WriteLine("Voulez vous garder cette ordre? (o pour oui, n pour non)");
        string reponse = Console.ReadLine() ?? "";
        if (reponse == "o")
        {
            list = cList;
            break;
        }
        else if (reponse == "n") break;
        Console.Clear();
    }
}