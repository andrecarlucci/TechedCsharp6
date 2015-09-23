using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadouken.Services {
    public abstract class Fighter {
        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        private Dictionary<string, ISpecialAttack> _specialAttacks;

        protected Fighter(string name) {
            if(String.IsNullOrEmpty(name)) {
                throw new ArgumentException("Name is mandatory", "Name");
            }
            _specialAttacks = new Dictionary<string, ISpecialAttack>();
        }

        protected void AddSpecialAttack(ISpecialAttack specialAttack) {
            _specialAttacks.Add(specialAttack.Name, specialAttack);
        }

        public void DoIt(string specialAttackName) {
            //_specialAttacks[specialAttackName].DoIt();
        }
    }

    public class Ryu {
        public string Name {
            get {
                return "Ryu";
            }
        }
        
        

    }

    public class Logger {
        private readonly string _filename;

        public Logger(string filename) {
            _filename = filename;
            if (String.IsNullOrEmpty(filename)) {
                throw new ArgumentException("Filename is mandatory", "filename");
            }
        }

        public void Debug(string message) {
            try {
                
            }
            catch(IOException ex) {
                
            }
        }
    }
}
