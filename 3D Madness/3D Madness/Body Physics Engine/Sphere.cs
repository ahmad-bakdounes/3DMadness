using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Sphere : Body
    {

        //=-----------data members--------= 
        protected float radius;


        protected float volume;


        protected float density;

       


        //=----------------methods-
        public Sphere() 
        {
            this.Radius = 1;
            this.position = new Vector3(0, 0, 0);
            Calculate_Volume();
            Calculate_Density(); 
        }

        public Sphere(float radius, Vector3 position )
        {
            this.Radius = radius;
            this.Position = position;
            Calculate_Volume();
            Calculate_Density(); 
        }

        public float Radius
        {
            get { return radius; }
            set
            {
                radius = value; Calculate_Volume();
                Calculate_Density();
            }
        }

        public float Volume
        {
            get { return volume; }
           
        }
        private void Calculate_Volume()
        {
         
            this.volume= (float)(Math.Pow(Radius,3) * (4/3) * (Math.PI)); 
        }
        public float Density
        {
            get { return density; }
            
        }
        private void Calculate_Density()
        {
            if (Volume != 0)
            {

                this.density = getMass() / Volume;
            }
        }

    }
}
