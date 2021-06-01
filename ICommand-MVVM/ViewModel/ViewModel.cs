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
            contacts.Add(new Contact() { Name = "Вова", Age = 30, Surname = "Зеленський", Phone = "+3809934532123", IsMale = true });
            contacts.Add(new Contact() { Name = "Рінат", Age = 25, Surname = "Ахметов", Phone = "+38037124224", IsMale = false });
            contacts.Add(new Contact() { Name = "Беня(Ігор)", Age = 33, Surname = "Коломойській", Phone = "+38063285792", IsMale = false });
            copyContactCommand = new DelegateCommand(DublicateSelectedContact, IsCanSetCreate);
            removeContactCommand = new DelegateCommand(RemoveSelectedContact, IsCanSetRemove);

            // встановлення обробника на подію зміни властивості 
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

        bool IsCanSetCreate()
        {
            return SelectedContact != null;
        }
        // Метод дублювання вибраного контакта
        public void DublicateSelectedContact()
        {
            if (SelectedContact != null)
            {
                contacts.Add((Contact)SelectedContact.Clone()); // on contacts func
            }
        }

        bool IsCanSetRemove()
        {
            return SelectedContact != null;
        }
        public void RemoveSelectedContact()
        {
            if (SelectedContact != null)
                contacts.Remove(SelectedContact);
        }

        bool IsCanOnlyCreate()
        {
            return true;
        }


        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
