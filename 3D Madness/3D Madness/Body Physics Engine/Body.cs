using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    abstract class Body
    {
        //=------------------data members--------=

        protected float inverseMass;
        protected Matrix3 inverseInertiaTensor;
        protected float linearDamping;
        protected float angularDamping;
        protected Vector3 position;
        protected Quaternion orientation;
        protected Vector3 velocity;
        protected Vector3 rotation;
        protected Matrix3 inverseInertiaTensorWorld;
        protected float motion;
        protected bool isAwake;
        protected bool canSleep;
        protected Matrix4 transformMatrix;
        protected Vector3 forceAccum;
        protected Vector3 torqueAccum;
        protected Vector3 acceleration;
        protected Vector3 lastFrameAcceleration;
        
        //=----------------------method---------=


        public void calculateDerivedData()
        {
            orientation.normalise();

            // Calculate the transform matrix for the body.
            _calculateTransformMatrix(transformMatrix, position, orientation);

            // Calculate the inertiaTensor in world space.
            _transformInertiaTensor(inverseInertiaTensorWorld,
                orientation,
                inverseInertiaTensor,
                transformMatrix);
        }
        public void integrate(float duration)
        {

           //  if (!isAwake) return;

            // Calculate linear acceleration from force inputs.
            lastFrameAcceleration = acceleration;
            lastFrameAcceleration.addScaledVector(forceAccum, inverseMass);

            // Calculate angular acceleration from torque inputs.
            Vector3 angularAcceleration =
                inverseInertiaTensorWorld.transform(torqueAccum);

            // Adjust velocities
            // Update linear velocity from both acceleration and impulse.
            velocity.addScaledVector(lastFrameAcceleration, duration);

            // Update angular velocity from both acceleration and impulse.
            rotation.addScaledVector(angularAcceleration, duration);

            // Impose drag.
            velocity *= real_pow(linearDamping, duration);
            rotation *= real_pow(angularDamping, duration);

            // Adjust positions
            // Update linear position.
            position.addScaledVector(velocity, duration);

            // Update angular position.
            orientation.addScaledVector(rotation, duration);

            // Normalise the orientation, and update the matrices with the new
            // position and orientation
            calculateDerivedData();

            // Clear accumulators.
            clearAccumulators();

            // Update the kinetic energy store, and possibly put the body to
            // sleep.
            if (canSleep) {
            real currentMotion = velocity.scalarProduct(velocity) +
                rotation.scalarProduct(rotation);

            real bias = real_pow(0.5, duration);
            motion = bias*motion + (1-bias)*currentMotion;

            if (motion < sleepEpsilon) setAwake(false);
            else  if (motion > 10 * sleepEpsilon) motion = 10 * sleepEpsilon;

        }

        public void setMass( float mass)
        {
            if(mass != 0){
                inverseMass = 1.0f / mass;
            }
            else{
                throw new Exception("mass =0 "); 
            }
        }
        public float getMass() 
        {
            if (inverseMass == 0) {
                return float.MaxValue;
            }else {
                return (1.0f) / inverseMass;
            }
        }

        public float InverseMass
        {
          get { return inverseMass; }
          set { inverseMass = value; }
        }
        public bool hasFiniteMass()
        {
            return inverseMass >= 0.0f;
        }
        public setInertiaTensor(Matrix3 inertiaTensor)
        {   
            inverseInertiaTensor.setInverse(inertiaTensor);
            _checkInverseInertiaTensor(inverseInertiaTensor);
        }

        public Matrix3 InverseInertiaTensor
        {
          get { return inverseInertiaTensor; }
          set 
          { 
              
              _checkInverseInertiaTensor(inverseInertiaTensor);
              inverseInertiaTensor = value;
          }
        }
        public Matrix3 getInertiaTensor() 
        {
        }


        public Matrix3 getInertiaTensorWorld 
        {
            //
        }



        public Matrix3 InverseInertiaTensorWorld
        {
          get { return inverseInertiaTensorWorld; }
        }



        public void setDamping(float linearDamping, float angularDamping) 
        {
            this.linearDamping = linearDamping;
            this.angularDamping = angularDamping;
        }
        public float LinearDamping
        {
          get { return linearDamping; }
          set { linearDamping = value; }
        }
        public float AngularDamping
        {
          get { return angularDamping; }
          set { angularDamping = value; }
        }
        public Vector3 Position
        {
          get { return position; }
          set { position = value; }
        }


        public Quaternion Orientation
        {
          get { return orientation; }
          set
          {
              orientation = value;
              orientation.normalise();
          }
        }
   


        //public void getTransform(Matrix4 transform) 
        //{

        //}
        //public void getTransform(float matrix[16]) 
        //{

        //}
        //public float[] getGLTransform() 
        //{
        //    float[] matrix =new float[16]; 
        //    matrix[0] = (float)transformMatrix.data[0];
        //    matrix[1] = (float)transformMatrix.data[4];
        //    matrix[2] = (float)transformMatrix.data[8];
        //    matrix[3] = 0;

        //    matrix[4] = (float)transformMatrix.data[1];
        //    matrix[5] = (float)transformMatrix.data[5];
        //    matrix[6] = (float)transformMatrix.data[9];
        //    matrix[7] = 0;

        //    matrix[8] = (float)transformMatrix.data[2];
        //    matrix[9] = (float)transformMatrix.data[6];
        //    matrix[10] = (float)transformMatrix.data[10];
        //    matrix[11] = 0;

        //    matrix[12] = (float)transformMatrix.data[3];
        //    matrix[13] = (float)transformMatrix.data[7];
        //    matrix[14] = (float)transformMatrix.data[11];
        //    matrix[15] = 1;
        //    return matrix ;
        //}
        public Matrix4 getTransform() 
        {
            return transformMatrix;
        }

        public Vector3 getPointInLocalSpace( Vector3 point) 
        {
            return transformMatrix.transformInverse(point);
        }
        public Vector3 getPointInWorldSpace( Vector3 point) 
        {
            return transformMatrix.transform(point);
        }     
        public Vector3 getDirectionInLocalSpace( Vector3 direction)
        {
            return transformMatrix.transformInverseDirection(direction);
        } 
        public Vector3 getDirectionInWorldSpace( Vector3 direction)
        {
            return transformMatrix.transformDirection(direction);
        } 
        public Vector3 Velocity
        {
          get { return velocity; }
          set { velocity = value; }
        }
        public void addVelocity( Vector3 deltaVelocity)
        {
             velocity += deltaVelocity;
        }
        public Vector3 Rotation
        {
          get { return rotation; }
          set { rotation = value; }
        }
        public void addRotation( Vector3 deltaRotation)
        {
            rotation += deltaRotation;
        }

        public bool getAwake() 
        {
            return isAwake;
        }
        public void setAwake(bool awake=true)
        {
           if (awake) {
            isAwake= true;
            // Add a bit of motion to avoid it falling asleep immediately.
            motion = sleepEpsilon*2.0f;
            }else {
                isAwake = false;
                velocity.clear();
                rotation.clear();
            }
        }
        public bool getCanSleep() 
        {
            return canSleep;
        }
        public void setCanSleep( bool canSleep=true)
        {
            this.canSleep = canSleep;

            if (!canSleep && !isAwake) setAwake();

        }

        public Vector3 getLastFrameAcceleration() 
        {
            return lastFrameAcceleration;
        }

        public void clearAccumulators()
        {
            forceAccum.clear();
            torqueAccum.clear();
        }

        public void addForce( Vector3 force)
        {
            forceAccum += force;
            isAwake = true;
        }

        public void addForceAtPoint( Vector3 force,  Vector3 point)
        {
             Vector3 pt = getPointInWorldSpace(point);
             addForceAtPoint(force, pt);
        }
        public void addForceAtBodyPoint( Vector3 force,  Vector3 point)
        {
             Vector3 pt = point;
             pt -= position;
             forceAccum += force;
             torqueAccum += pt % force;
             isAwake = true;
        }
        public void addTorque( Vector3 torque)
        {

            torqueAccum += torque;
            isAwake = true;

        }
        public Vector3 Acceleration
        {
          get { return acceleration; }
          set { acceleration = value; }
        }
    }
}
