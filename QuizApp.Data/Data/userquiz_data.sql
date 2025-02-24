INSERT INTO UserQuizzes (Id, UserId, QuizId, QuizCode, StartedAt, FinishedAt)
VALUES 
  (NEWID(), '6a5e2c3b-4f92-4cbb-a28f-5a9a12345678', '9f0e4d2a-1c33-476c-9b4e-5f44d1234567', 'QUIZ-001', GETDATE(), NULL),
  (NEWID(), '7d3b4e2c-5f92-4ebb-a19f-8a9a76543210', '3a7c9f1b-2b44-48f7-8cde-6f55a7654321', 'QUIZ-002', GETDATE(), NULL);
