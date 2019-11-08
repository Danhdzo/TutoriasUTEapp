namespace TutoriasUTEapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 25),
                        UserPassword = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Career",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Abbreviation = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClassGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupID = c.String(nullable: false, maxLength: 45),
                        CareerID = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        Section = c.String(nullable: false, maxLength: 2),
                        ModalityID = c.Int(nullable: false),
                        TurnID = c.Int(nullable: false),
                        Generation = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Career", t => t.CareerID, cascadeDelete: true)
                .ForeignKey("dbo.Modality", t => t.ModalityID, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .ForeignKey("dbo.Turn", t => t.TurnID, cascadeDelete: true)
                .Index(t => t.CareerID)
                .Index(t => t.ModalityID)
                .Index(t => t.TurnID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.ClassGroupCourse",
                c => new
                    {
                        ClassGroupID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassGroupID, t.CourseID })
                .ForeignKey("dbo.ClassGroup", t => t.ClassGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.ClassGroupID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 255),
                        Units = c.Int(nullable: false),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.StudentCourseComment",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => new { t.StudentID, t.CourseID, t.Date })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Registration = c.String(nullable: false, maxLength: 15),
                        FirstMidName = c.String(nullable: false, maxLength: 35),
                        LastNameP = c.String(nullable: false, maxLength: 30),
                        LastNameM = c.String(nullable: false, maxLength: 30),
                        Telephone = c.String(maxLength: 20),
                        EmergencyTelephone = c.String(nullable: false, maxLength: 20),
                        AcademicEmail = c.String(nullable: false, maxLength: 45),
                        Birthday = c.String(nullable: false, maxLength: 20),
                        SituationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Situation", t => t.SituationID, cascadeDelete: true)
                .Index(t => t.SituationID);
            
            CreateTable(
                "dbo.ClassGroupStudent",
                c => new
                    {
                        ClassGroupID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassGroupID, t.StudentID })
                .ForeignKey("dbo.ClassGroup", t => t.ClassGroupID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.ClassGroupID)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Situation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentCourseGrade",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Grade = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => new { t.StudentID, t.CourseID, t.Unit })
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(nullable: false, maxLength: 8),
                        FirstMidName = c.String(nullable: false, maxLength: 35),
                        LastNameP = c.String(nullable: false, maxLength: 30),
                        LastNameM = c.String(nullable: false, maxLength: 30),
                        UserName = c.String(nullable: false, maxLength: 30),
                        UserPassword = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeacherRole",
                c => new
                    {
                        TeacherID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherID, t.RoleID })
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reminder",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 255),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Modality",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Turn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassGroup", "TurnID", "dbo.Turn");
            DropForeignKey("dbo.ClassGroup", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.ClassGroup", "ModalityID", "dbo.Modality");
            DropForeignKey("dbo.ClassGroupCourse", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.TeacherRole", "TeacherID", "dbo.Teacher");
            DropForeignKey("dbo.TeacherRole", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Reminder", "RoleID", "dbo.Role");
            DropForeignKey("dbo.StudentCourseComment", "StudentID", "dbo.Student");
            DropForeignKey("dbo.StudentCourseGrade", "StudentID", "dbo.Student");
            DropForeignKey("dbo.StudentCourseGrade", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Student", "SituationID", "dbo.Situation");
            DropForeignKey("dbo.ClassGroupStudent", "StudentID", "dbo.Student");
            DropForeignKey("dbo.ClassGroupStudent", "ClassGroupID", "dbo.ClassGroup");
            DropForeignKey("dbo.StudentCourseComment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.ClassGroupCourse", "ClassGroupID", "dbo.ClassGroup");
            DropForeignKey("dbo.ClassGroup", "CareerID", "dbo.Career");
            DropIndex("dbo.Reminder", new[] { "RoleID" });
            DropIndex("dbo.TeacherRole", new[] { "RoleID" });
            DropIndex("dbo.TeacherRole", new[] { "TeacherID" });
            DropIndex("dbo.StudentCourseGrade", new[] { "CourseID" });
            DropIndex("dbo.StudentCourseGrade", new[] { "StudentID" });
            DropIndex("dbo.ClassGroupStudent", new[] { "StudentID" });
            DropIndex("dbo.ClassGroupStudent", new[] { "ClassGroupID" });
            DropIndex("dbo.Student", new[] { "SituationID" });
            DropIndex("dbo.StudentCourseComment", new[] { "CourseID" });
            DropIndex("dbo.StudentCourseComment", new[] { "StudentID" });
            DropIndex("dbo.Course", new[] { "TeacherID" });
            DropIndex("dbo.ClassGroupCourse", new[] { "CourseID" });
            DropIndex("dbo.ClassGroupCourse", new[] { "ClassGroupID" });
            DropIndex("dbo.ClassGroup", new[] { "TeacherID" });
            DropIndex("dbo.ClassGroup", new[] { "TurnID" });
            DropIndex("dbo.ClassGroup", new[] { "ModalityID" });
            DropIndex("dbo.ClassGroup", new[] { "CareerID" });
            DropTable("dbo.Log");
            DropTable("dbo.Turn");
            DropTable("dbo.Modality");
            DropTable("dbo.Reminder");
            DropTable("dbo.Role");
            DropTable("dbo.TeacherRole");
            DropTable("dbo.Teacher");
            DropTable("dbo.StudentCourseGrade");
            DropTable("dbo.Situation");
            DropTable("dbo.ClassGroupStudent");
            DropTable("dbo.Student");
            DropTable("dbo.StudentCourseComment");
            DropTable("dbo.Course");
            DropTable("dbo.ClassGroupCourse");
            DropTable("dbo.ClassGroup");
            DropTable("dbo.Career");
            DropTable("dbo.Administrator");
        }
    }
}
