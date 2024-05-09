CREATE SCHEMA itba;

CREATE TABLE competences (
                             competence_id VARCHAR(50) PRIMARY KEY,
                             competence VARCHAR(500)
);

CREATE TABLE outcomes (
                                 outcome_id INT AUTO_INCREMENT PRIMARY KEY,
                                 outcome VARCHAR(250)
);

CREATE TABLE courses (
                         course_id VARCHAR(50) PRIMARY KEY,
                         english_title VARCHAR(100),
                         ukrainian_title VARCHAR(100),
                         credits INT,
                         total_hours INT,
                         class_hours INT,
                         lecture_hours INT,
                         practice_hours INT,
                         self_study_hours INT,
                         passfail INT,
                         type VARCHAR(50)
);

CREATE TABLE courses_competences (
                                    course_id VARCHAR(50),
                                    competence_id VARCHAR(50),
                                    PRIMARY KEY (course_id, competence_id),
                                    FOREIGN KEY (course_id) REFERENCES courses(course_id) ON DELETE CASCADE,
                                    FOREIGN KEY (competence_id) REFERENCES competences(competence_id) ON DELETE CASCADE 
);
CREATE TABLE courses_outcomes (
                                    course_id VARCHAR(50),
                                    outcome_id INT,
                                    PRIMARY KEY (course_id, outcome_id),
                                    FOREIGN KEY (course_id) REFERENCES courses(course_id) ON DELETE CASCADE,
                                    FOREIGN KEY (outcome_id) REFERENCES outcomes(outcome_id) ON DELETE CASCADE
);

CREATE TABLE terms (
                       term_id INT AUTO_INCREMENT PRIMARY KEY,
                       name VARCHAR(30)
);

CREATE TABLE programs (
                          program_id INT AUTO_INCREMENT PRIMARY KEY,
                          program_name VARCHAR(30)
);

CREATE TABLE programs_terms_courses (
                                        program_id INT,
                                        term_id INT,
                                        course_id  VARCHAR(50),
                                        PRIMARY KEY (program_id, term_id, course_id),
                                        FOREIGN KEY (program_id) REFERENCES programs(program_id) ON DELETE CASCADE,
                                        FOREIGN KEY (term_id) REFERENCES terms(term_id),
                                        FOREIGN KEY (course_id) REFERENCES courses(course_id) ON DELETE CASCADE
);

CREATE TABLE programs_terms_duration (
                                       program_id INT,
                                       term_id INT,
                                       start_date DATE,
                                       finish_date DATE,
                                       duration INT,
                                       PRIMARY KEY (program_id, term_id),
                                       FOREIGN KEY (program_id) REFERENCES programs(program_id) ON DELETE CASCADE,
                                       FOREIGN KEY (term_id) REFERENCES terms(term_id)
);

CREATE TABLE programs_year_terms (
                                         program_id INT,
                                         term_id INT,
                                         year INT,
                                         PRIMARY KEY (program_id, term_id),
                                         FOREIGN KEY (program_id) REFERENCES programs(program_id) ON DELETE CASCADE,
                                         FOREIGN KEY (term_id) REFERENCES terms(term_id)
);

drop table programs_terms_duration;
ALTER TABLE courses MODIFY type CHAR(1);


