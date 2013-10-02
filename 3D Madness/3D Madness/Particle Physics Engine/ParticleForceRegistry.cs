using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3D_Madness
{
    class ParticleForceRegistry
    {

        //=-------------------------------data member---=
        public struct ParticleForceRegistration
        {
           public Particle particle;
           public ParticleForceGenerator fg;
        };

        public List<ParticleForceRegistration> Registry;  

        //=-----------------------------------method----=
        public void add(Particle particle, ParticleForceGenerator fg) {


            ParticleForceRegistration temp = new ParticleForceRegistration(); 
            temp.particle=particle; 
            temp.fg=fg;
            Registry.Add(temp); 
        
        }
        public void remove(Particle particle, ParticleForceGenerator fg) { 


            ParticleForceRegistration temp = new ParticleForceRegistration(); 
            temp.particle=particle; 
            temp.fg=fg;
            Registry.Remove(temp);
        }
        public void clear(){
        
        Registry = new List<ParticleForceRegistration>();
        }
        void updateForces(float duration){

            ParticleForceRegistration temp;  

            for (int i =0 ; i != Registry.Count; i++)
            {
                temp= Registry.ElementAt(i);
                temp.fg.updateForce(temp.particle, duration);
            }

        }


    }
}
