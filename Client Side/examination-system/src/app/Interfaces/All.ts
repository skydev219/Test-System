export interface IExam {
  id: number;
  name: string;
  admin: Admin;
  grades: Grade[];
  questions: Question[];
}

export class Exam implements IExam {
  id: number;
  name: string;
  admin: Admin;
  grades: Grade[];
  questions: Question[];

  /**
   *
   */
  constructor(
    id: number,
    name: string,
    admin: Admin,
    grades: Grade[],
    questions: Question[]
  ) {
    this.id = id;
    this.name = name;
    this.admin = admin;
    this.grades = grades;
    this.questions = questions;
  }
}

export interface Question {
  id: number;
  name: string;
  exam_ID: number;
  exam: string;
  answer: Answer;
}

export interface Answer {
  id: number;
  name: string;
  q_ID: number;
  q_IDNavigation: string;
}

export interface Grade {
  exam_ID: number;
  st_ID: number;
  grade1: number;
  exam: string;
  st: Student;
}

interface Admin {
  id: number;
  name: string;
  userName: string;
  pass: string;
  exams: string[];
  students: Student[];
}

export interface Student {
  id: number;
  name: string;
  userName: string;
  pass: string;
  admin_ID: number;
  admin: string;
  grades: string[];
}
