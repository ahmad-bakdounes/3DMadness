using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3D_Madness
{
   public interface ParticleForceGenerator
    {
         void updateForce(Particle particle, float duration) ;
    }
}
