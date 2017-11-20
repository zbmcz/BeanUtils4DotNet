using System;
using System.Collections.Generic;

namespace Abchina.Ebiz.Tools.BeanUtils.Test
{
    class MainClass
    {
        /// <summary>
        /// TestCase BeanUtils
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Student stu = new Student();
            stu.Name = "Giraffe";
            stu.Company = "ABC";
            stu._school = "HEBUT";

            Student stu1 = BeanUtils.CloneBean<Student>(stu,new object[1]{"BeiDa"});
            Console.WriteLine(stu1.ToString());

            BeanUtils.CopyProperty(stu1,"Company","H3C");
            Console.WriteLine(stu1.ToString());

            Student stu2 = new Student();
            BeanUtils.CopyPorperties(stu2,stu1);
            Console.WriteLine(stu2.ToString());

            Dictionary<string,object> dic = BeanUtils.Describe(stu2);
            foreach(var item in dic){
                Console.WriteLine("----" + item.Key + "----" + item.Value + "----");
            }

            Console.ReadKey();
        }
    }
    class Student
    {

        public string Name{
            get;set;
        }
        private string Sex{
            get;set;
        }

        private string _company;
        public string Company { 
            get{
                return this._company;
            }
            set{
                this._company = value;
            }
        }

        public string _school;

        public override string ToString()
        {
            return string.Format("[Student: Name={0}, Company={1} , _school={2}]", Name, Company,this._school);
        }


        public Student(string school){
            this._school = school;
        }
        public Student(){
            
        }

    }
}
