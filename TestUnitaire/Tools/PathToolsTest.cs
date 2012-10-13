using System;
using Tagomatique.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUnitaire.Tools
{
    /// <summary>
    ///Classe de test pour PathToolsTest, destinée à contenir tous
    ///les tests unitaires PathToolsTest
    ///</summary>
    [TestClass]
    public class PathToolsTest
    {
        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region GenerateRelativePathTest - V1

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_1()
        {
            const string mainDirPath = @"C:\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2\NomFichier.txt";
            const string expected = @".\Dossier1\Dossier2\NomFichier.txt";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_2()
        {
            const string mainDirPath = @"C:\Dossier1";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2\NomFichier.txt";
            const string expected = @".\Dossier2\NomFichier.txt";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_3()
        {
            const string mainDirPath = @"C:\Dossier1\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2\NomFichier.txt";
            const string expected = @".\Dossier2\NomFichier.txt";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_4()
        {
            const string mainDirPath = @"C:\Dossier1\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2\";
            const string expected = @".\Dossier2\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_5()
        {
            const string mainDirPath = @"C:\Dossier1\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2";
            const string expected = @".\Dossier2\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_6()
        {
            const string mainDirPath = @"C:\Dossier1\Dossier2\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2_v2\Dossier3";
            const string expected = @"..\Dossier2_v2\Dossier3\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_7()
        {
            const string mainDirPath = @"C:\Dossier1_c\Dossier2_c\";
            const string absoluteFilePath = @"D:\Dossier1_d\Dossier2_d\Dossier3_d";
            const string expected = @"D:\Dossier1_d\Dossier2_d\Dossier3_d\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_8()
        {
            const string mainDirPath = @"C:\Dossier1_c\Dossier2_c\";
            const string absoluteFilePath = @"D:\Dossier1_d\Dossier2_d\Dossier3_d\";
            const string expected = @"D:\Dossier1_d\Dossier2_d\Dossier3_d\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest1_9()
        {
            const string mainDirPath = @"C:\Dossier1\Dossier2\Dossier3b\Dossier4b\";
            const string absoluteFilePath = @"C:\Dossier1\Dossier2\Dossier3\Dossier4\Dossier5\";
            const string expected = @"..\..\Dossier3\Dossier4\Dossier5\";

            string actual = PathTools.GenerateRelativePath(mainDirPath, absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GenerateRelativePathTest - V2

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest2_1()
        {
            string absoluteFilePath = Environment.CurrentDirectory + @"\Dossier1\Dossier2\NomFichier.txt";
            const string expected = @".\Dossier1\Dossier2\NomFichier.txt";

            string actual = PathTools.GenerateRelativePath(absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest2_2()
        {
            string absoluteFilePath = Environment.CurrentDirectory + @"\Dossier1\Dossier2\NomFichier.txt";
            const string expected = @".\Dossier1\Dossier2\NomFichier.txt";

            string actual = PathTools.GenerateRelativePath(absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest2_3()
        {
            string absoluteFilePath = Environment.CurrentDirectory + @"\Dossier1\Dossier2\";
            const string expected = @".\Dossier1\Dossier2\";

            string actual = PathTools.GenerateRelativePath(absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest2_4()
        {
            string absoluteFilePath = Environment.CurrentDirectory + @"\Dossier1\Dossier2";
            const string expected = @".\Dossier1\Dossier2\";

            string actual = PathTools.GenerateRelativePath(absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test pour GenerateRelativePath
        /// </summary>
        [TestMethod]
        public void GenerateRelativePathTest2_5()
        {
            const string absoluteFilePath = @"D:\Dossier1_d\Dossier2_d\Dossier3_d\";
            const string expected = @"D:\Dossier1_d\Dossier2_d\Dossier3_d\";

            string actual = PathTools.GenerateRelativePath(absoluteFilePath);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}