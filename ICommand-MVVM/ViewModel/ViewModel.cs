using ICommand_MVVM.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICommand_MVVM
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Contact selectedContact;
        // collections with contacts
        private readonly ICollection<Contact> contacts = new ObservableCollection<Contact>();
       
        private Command copyContactCommand;
        private Command removeContactCommand;

        public ViewModel()
        {
            contacts.Add(new Contact() { Name = "Вова", Age = 30, Surname = "Зеленський", Phone = "+3809934532123", City="Кривий ріг" ,IsMale = true, PathToImage= "C:/Users/vadim_oyanwuw/source/repos/ICommand-MVVM/ICommand-MVVM/sasha.png" });
            contacts.Add(new Contact() { Name = "Рінат", Age = 25, Surname = "Ахметов", Phone = "+38037124224", City = "Донбасс", IsMale = false, PathToImage= "C:/Users/vadim_oyanwuw/source/repos/ICommand-MVVM/ICommand-MVVM/ahmetov.png" });
            contacts.Add(new Contact() { Name = "Беня(Ігор)", Age = 33, Surname = "Коломойській", Phone = "+38063285792", City = "Дніпро", IsMale = false, PathToImage= "C:/Users/vadim_oyanwuw/source/repos/ICommand-MVVM/ICommand-MVVM/privat.png" });
            copyContactCommand = new DelegateCommand(DublicateSelectedContact, IsCanSetCreate);
            removeContactCommand = new DelegateCommand(RemoveSelectedContact, IsCanSetRemove);

            PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == nameof(SelectedContact))
                {
                    copyContactCommand.RaiseCanExecuteChanged();
                    removeContactCommand.RaiseCanExecuteChanged();
                }
            };
        }

        // for binding elements on windows
        public IEnumerable<Contact> Contacts => contacts;

        // change property
        public Contact SelectedContact
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                OnPropertyChanged();
            }
        }

        public ICommand CopyContactCommand => copyContactCommand;
        public ICommand RemoveContactCommand => removeContactCommand;

        // CREATE
        bool IsCanSetCreate()
        {
            return SelectedContact != null;
        }
        public void DublicateSelectedContact()
        {
            if (SelectedContact != null)
            {
                SelectedContact.AddImage();
                contacts.Add((Contact)SelectedContact.Clone()); // on contacts func
            }
        }

        // REMOVE
        bool IsCanSetRemove()
        {
            return SelectedContact != null;
        }
        public void RemoveSelectedContact()
        {
            if (SelectedContact != null)
                contacts.Remove(SelectedContact);
        }

        // PROPERTY CHANGED EVENTS
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
