using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _3D_Madness
{
    public class Particle
    {
        //=-------------------data members-----=
        private float _damping;
        private float _inverseMass;
        private Vector3 _position;
        private Vector3 _velocity;
        private Vector3 _acceleration;
        private Vector3 _forceAccum;

        //setter and getters
        public float InverseMass
        {
            get { return _inverseMass; }
            set { _inverseMass = value; }
        }

        public float Damping
        {
            get { return _damping; }
            set { _damping = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position.X = value.X; _position.Z = value.Z; _position.Y = value.Y; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity.X = value.X; _velocity.Z = value.Z; _velocity.Y = value.Y; }
        }

        public Vector3 Acceleration
        {
            get { return _acceleration; }
            set { _acceleration.X = value.X; _acceleration.Y = value.Y; _acceleration.Z = value.Z; }
        }

        public Vector3 ForceAccum
        {
            get { return _forceAccum; }
            set { _forceAccum.X = value.X; _forceAccum.Y = value.Y; _forceAccum.Z = value.Z; }
        }


        //=---------------------methods---------=

        public float getMass()
        {
            if (InverseMass==0)
            {
                return float.MaxValue;
            }
            return 1/this.InverseMass;
        }

        protected void setMass(float mass)
        {
            if (mass != 0)
            {
                this.InverseMass = 1/mass;
            }
            else
            {
                //assert
                throw new Exception("Zero Mass");
            }
        }

        void integrate(float duration)
        {
            if (this.InverseMass <= 0.0f) return;

            //assert(duration > 0.0);
            if(duration<=0)
                throw new Exception("Negative Duration");

            // Update linear position.
            this.Position += this.Velocity * duration;

            // Work out the acceleration from the force
            Vector3 resultingAcc = this.Acceleration;
            resultingAcc += this.ForceAccum*this.InverseMass;

            // Update linear velocity from the acceleration.
            this.Velocity += resultingAcc * duration ;

            // Impose drag.
            this.Velocity *= (float)Math.Pow(this.Damping, duration);

            // Clear the forces.
            clearAccumulator();
        }

        public bool hasFiniteMass()
        {
            if (this.InverseMass >= 0)
                return true;
            return false;
        }

        void clearAccumulator()
        {
            this.ForceAccum = new Vector3(0,0,0);
        }

        public void addForce(Vector3 force)
        {
            this.ForceAccum += force;
        }
    }
}
