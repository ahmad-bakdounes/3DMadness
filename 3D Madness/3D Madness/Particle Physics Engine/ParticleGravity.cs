using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3D_Madness 
{
    class ParticleGravity : ParticleForceGenerator
    {
        //=------------------data member------=
        private Vector3 gravity;

        //=------------------method-----------=


        ParticleGravity(Vector3 gravity)
        {
            this.Gravity = gravity;
        }

        public Vector3 Gravity
        {
            get { return gravity; }
            set { gravity.X = value.X; gravity.Y = value.Y; gravity.Z = value.Z; }
        }

        void ParticleForceGenerator.updateForce(Particle particle, float duration)
        {
            // Check that we do not have infinite mass
                 if (!particle.hasFiniteMass())
                     return;

            // Apply the mass-scaled force to the particle
                 particle.addForce(Gravity * particle.getMass());
        }
    }

         
    
}
