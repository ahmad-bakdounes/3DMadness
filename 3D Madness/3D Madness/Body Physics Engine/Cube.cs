using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Cube : Body 
    {
        //=------------ data members ------=
        protected float high;
        protected float width;
        protected float length;
        protected float volume;
        protected float density;


        //=--------------methods---------= 


        public Cube()
        {
            high = width = length = 1;
            this.Position = new vector3(0,0,0);
            Calculate_Volume();
            Calculate_Density();
        }

        public Cube(float high , float width , float length, vector3 position ,float mass ) 
        {
            this.high = high;
            this.width = width;
            this.length = length;
            this.Position = position;
            this.setMass(mass);
            Calculate_Volume(); 
            Calculate_Density();
           

        }

        public float High
        {
            get { return high; }
            set
            {
                high = value; Calculate_Volume();
                Calculate_Density();
            }
        }
       

        public float Length
        {
            get { return length; }
            set
            {
                length = value; Calculate_Volume();
                Calculate_Density();
            }
        }
        

        public float Width
        {
            get { return width; }
            set
            {
                width = value; Calculate_Volume();
                Calculate_Density();
            }
        }


        public float Volume
        {
            get { return volume; }
      
        }
        private void Calculate_Volume() 
        {
            this.volume = high * width * length;
        }
        

        public float Density
        {
            get { return density; }
        }

        private void Calculate_Density() 
        {
            if (Volume != 0) {

                this.density = getMass() / Volume ;
            }
        }


    }
}
