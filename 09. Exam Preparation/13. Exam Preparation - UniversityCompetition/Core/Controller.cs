using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;


        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            IStudent hadThatStudent = students.Models.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            if (hadThatStudent != null)
            {
                return $"{firstName} {lastName} is already added in the repository.";
            }
            IStudent newStudent = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(newStudent);
            return $"Student {firstName} {lastName} is added to the {nameof(StudentRepository)}!";
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) && subjectType != nameof(HumanitySubject) && subjectType != nameof(TechnicalSubject))
            {
                return $"Subject type {subjectType} is not available in the application!";
            }
            ISubject hadThatSubject = subjects.Models.FirstOrDefault(s => s.Name == subjectName);
            if (hadThatSubject != null)
            {
                return $"{subjectName} is already added in the repository.";
            }
            Subject subject = null;
            if (subjectType == nameof(EconomicalSubject))
            {
                subject = new EconomicalSubject(subjectName, subjects.Models.Count + 1);
            }
            else if (subjectType == nameof(HumanitySubject))
            {
                subject = new HumanitySubject(subjectName, subjects.Models.Count + 1);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                subject = new TechnicalSubject(subjectName, subjects.Models.Count + 1);
            }
            subjects.AddModel(subject);
            return $"{subjectType} {subjectName} is created and added to the {nameof(SubjectRepository)}!";
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            IUniversity hadThatUni = universities.Models.FirstOrDefault(u => u.Name == universityName);
            if (hadThatUni != null)
            {
                return $"{universityName} is already added in the repository.";
            }
            List<int> numberRequiredSubject = new List<int>();
            foreach (var name in requiredSubjects)
            {
                ISubject subject = subjects.FindByName(name);
                numberRequiredSubject.Add(subject.Id);
            }
            IUniversity newUniversity = new University(universities.Models.Count + 1, universityName, category, capacity, numberRequiredSubject);
            universities.AddModel(newUniversity);
            return $"{universityName} university is created and added to the {nameof(UniversityRepository)}!";
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentNameSplitted = studentName.Split();
            IStudent student = students.Models.FirstOrDefault(s => s.FirstName == studentNameSplitted[0] && s.LastName == studentNameSplitted[1]);
            if (student == null) 
            {
                return $"{studentNameSplitted[0]} {studentNameSplitted[1]} is not registered in the application!";
            }
            IUniversity uni = universities.Models.FirstOrDefault(s => s.Name == universityName);
            if (uni == null)
            {
                return $"{universityName} is not registered in the application!";
            }
            foreach (var test in uni.RequiredSubjects)
            {
                if (!student.CoveredExams.Contains(test))
                {
                    return $"{studentName} has not covered all the required exams for {universityName} university!";
                }
            }
            if (student.University != null)
            {
                if (student.University.Name == universityName)
                {
                    return $"{student.FirstName} {student.LastName} has already joined {universityName}.";
                }
            }

            IUniversity actualUni = universities.FindByName(universityName);
            student.JoinUniversity(actualUni);
            return $"{student.FirstName} {student.LastName} joined {universityName} university!";
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent hadThatStudent = students.FindById(studentId);
            if (hadThatStudent == null)
            {
                return $"Invalid student ID!";
            }
            ISubject hadThatSubject = subjects.FindById(subjectId);
            if (hadThatSubject == null)
            {
                return $"Invalid subject ID!";
            }
            if (hadThatStudent.CoveredExams.Contains(subjectId))
            {
                return $"{hadThatStudent.FirstName} {hadThatStudent.LastName} has already covered exam of {hadThatSubject.Name}.";
            }
            hadThatStudent.CoverExam(subjects.FindById(subjectId));
            return $"{hadThatStudent.FirstName} {hadThatStudent.LastName} covered {hadThatSubject.Name} exam!";
        }

        public string UniversityReport(int universityId)
        {
            StringBuilder sb = new();
            IUniversity currentUni = universities.FindById(universityId);
            sb.AppendLine($"*** {currentUni.Name} ***");
            sb.AppendLine($"Profile: {currentUni.Category}");
            int counter = 0;
            foreach (var student in students.Models)
            {
                if (student.University is null)
                {
                    continue;
                }
                if (student.University.Name == currentUni.Name)
                {
                    counter++;
                }
            }
            sb.AppendLine($"Students admitted: {counter}");
            sb.AppendLine($"University vacancy: {currentUni.Capacity - counter}");
            return sb.ToString().Trim();
        }
    }
}
