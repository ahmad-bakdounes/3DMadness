using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class ForceRegistry
    {

        //=-----------------------data members-------------=
        protected
        struct ForceRegistration
        {
            public Body body;
            public ForceGenerator fg;
        };

        protected List<ForceRegistration> registry;


        //=-----------------------methods------------------=

        public void add(Body body, ForceGenerator fg) { 
        
            ForceRegistration temp = new ForceRegistration; 
            temp.body=body; 
            temp.fg=fg;
            registry.Add(temp);
        
        }

        public void remove(Body body, ForceGenerator fg) { 
        
            ForceRegistration temp = new ForceRegistration; 
            temp.body=body; 
            temp.fg=fg;
            registry.Remove(temp);

        }

        public void clear() {
        
            registry =new List<ForceRegistration> ;

        }

        public void updateForces(float duration) {

            ForceRegistration temp;  

            for (int i=0 ; i < registry.Count; i++)
            {
                temp = registry.ElementAt(i);
                temp.fg.updateForce(temp.body, duration);
            }
        }

    }
}

