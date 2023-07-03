const BASE_URL = 'https://localhost:7014/api';
export const environment = {
  production: true,
  contentful: {
    loginAdmin: `${BASE_URL}/Admins/Login`,
    admin: `${BASE_URL}/Admins`,
    answer: `${BASE_URL}/Amswers`,
    exam: `${BASE_URL}/Exams`,
    grade: `${BASE_URL}/Grades`,
    getGradeByStudent: `${BASE_URL}/Grades/Student`,
    getGradeByExam: `${BASE_URL}/Grades/exam`,
    question: `${BASE_URL}/Questions`,
    loginStudent: `${BASE_URL}/Students/Login`,
    registerStudent: `${BASE_URL}/Students/Register`,
    student: `${BASE_URL}/Students`,
  },
};
