using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ICommand_MVVM
{
    public class Contact : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Name
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(); // call event for update name property
                OnPropertyChanged(nameof(Fullname));
            }

        }

        // Surname
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if(surname != value)
                {
                    surname = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Fullname));
                }
            }
        }

        // Age
        private int age;
        public int Age
        {
            get { return age; }
            set
            {
               if(age < 100 && age != value)
                {
                    age = value;
                    OnPropertyChanged();
                }
            }
        }

        // Phone
        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if(phone != value)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }

        // City
        

        // isMale
        private bool isMale;
        public bool IsMale
        {
            get { return isMale; }
            set
            {
                if (isMale != value)
                {
                    isMale = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(GenderName));
                }
            }
        }

        public string GenderName => IsMale ? "Male" : "Female";

        public string Fullname => $"{Name} {Surname}";

        public object Clone()
        {
            // shallow copy (поверхневе копіювання) - копіюються лише 
            // значення value типів та посилання reference типів
            Contact clone = (Contact)this.MemberwiseClone();

            // deep copy (глибоке копіювання) - кожний reference тип
            // копіюється власним методом клонування
            clone.Name = (string)this.Name.Clone();
            clone.Surname = (string)this.Surname.Clone();
            clone.Phone = (string)this.Phone.Clone();

            return clone;
        }

        // func for change properties callermemebers
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
