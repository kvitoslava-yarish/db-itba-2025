LOAD DATA INFILE '/var/lib/mysql-files/itba2025_courses.csv'
    INTO TABLE courses
    FIELDS TERMINATED BY ','
    ENCLOSED BY '"'
    LINES TERMINATED BY '\n'
    IGNORE 1 LINES
    (course_id, @titles, credits, total_hours, class_hours, lecture_hours, practice_hours, self_study_hours, passfail, type)
    SET english_title = SUBSTRING_INDEX(@titles, '/', 1),
        ukrainian_title = SUBSTRING_INDEX(@titles, '/', -1);
