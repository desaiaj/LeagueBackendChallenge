using League.BackendChallenge;

namespace LeagueBackendChallengeTests
{
    [TestClass]
    public class LeagueBackendChallengeTests
    {
        private readonly string fileNotFoundErrorMessage = "File not found";
        private readonly string invalidArgumentErrorMessage = "Provided file path is not valid";
        private readonly string invalidDataErrorMessage = "Provided Matrix is invalid, expects same number of Row N x Col M of Integers only";
        private readonly string invalidMatrixErrorMessage = "Provided matrix is invalid, please check the input";
        private readonly string validFilePath = @"matrix.csv";
        private readonly string InValidMatrix_FilePath = @"invalid_matrix.csv";

        [TestMethod]
        public void TryReadFile_With_ValidPath_Should_Pass()
        {
            string[] expectedOutPut = { "1,2,3", "4,5,6", "7,8,9" };

            var sut = new LeagueBackendChallenge();
            var result = sut.ReadFile_Filtered_SpaceOrEmpty(validFilePath);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOutPut.Length, result.Length);
            CollectionAssert.AreEqual(expectedOutPut, result);
        }

        [TestMethod]
        public void TryReadFile_With_InValidPath_Should_Throw_FileNotFound_Ex()
        {
            try
            {
                string inValidFilePath = @"C:\My Work\Learning\LeagueBackendChallenge\matrix112.csv";
                var sut = new LeagueBackendChallenge();
                var result = sut.ReadFile_Filtered_SpaceOrEmpty(inValidFilePath);
                Assert.IsNull(result);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                StringAssert.Contains(ex.Message, fileNotFoundErrorMessage);
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, fileNotFoundErrorMessage);
            }
        }

        [TestMethod]
        public void TryReadFile_With_NoPath_Should_Throw_ArgumentNull_Ex()
        {
            try
            {
                var sut = new LeagueBackendChallenge();
                var result = sut.ReadFile_Filtered_SpaceOrEmpty(path: null);
                Assert.IsNull(result);
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, invalidArgumentErrorMessage);
            }
        }

        [TestMethod]
        public void TryInvertMatrix_With_ValidMatrix_Should_Pass()
        {
            List<List<int>> expectedOutPut = new List<List<int>>
            {
                new List<int> { 1, 4, 7 },
                new List<int> { 2, 5, 8 },
                new List<int> { 3, 6, 9 }
            };

            List<List<int>> matrix = new List<List<int>>();
            var sut = new LeagueBackendChallenge();
            var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(validFilePath);
            sut.PrepareMatrix(fileData, matrix);
            var resultMatrix = sut.InvertMatrix(matrix);

            Assert.AreEqual(expectedOutPut.Count, resultMatrix.Count);
            Equals(expectedOutPut, resultMatrix);
        }

        [TestMethod]
        public void TryInvertMatrix_With_InValidMatrix_Should_Throw_InvalidDataEx()
        {
            try
            {
                var sut = new LeagueBackendChallenge();
                List<List<int>> matrix = new List<List<int>>();

                var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(InValidMatrix_FilePath);
                sut.PrepareMatrix(fileData, matrix);
                var resultMatrix = sut.InvertMatrix(matrix);

                Assert.IsNull(resultMatrix);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, invalidDataErrorMessage);
            }
        }

        [TestMethod]
        public void TryFlattenMatrix_With_ValidMatrix_Should_Pass()
        {
            try
            {
                string expectedOutPut = "1,2,3,4,5,6,7,8,9";

                List<List<int>> matrix = new List<List<int>>();
                var sut = new LeagueBackendChallenge();
                var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(validFilePath);
                sut.PrepareMatrix(fileData, matrix);
                var resultMatrix = sut.FlattenMatrix(matrix);

                Assert.AreEqual(expectedOutPut, resultMatrix);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, invalidDataErrorMessage);
            }
        }

        [TestMethod]
        public void TryFlattenMatrix_With_InValidMatrix_Should_Throw_InvalidDataEx()
        {
            try
            {
                var sut = new LeagueBackendChallenge();
                List<List<int>> matrix = new List<List<int>>();

                var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(InValidMatrix_FilePath);
                sut.PrepareMatrix(fileData, matrix);
                var resultMatrix = sut.FlattenMatrix(matrix);

                Assert.IsNull(resultMatrix);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, invalidDataErrorMessage);
            }
        }

        [TestMethod]
        public void TrySumOfMatrix_With_ValidMatrix_Should_Pass()
        {
            try
            {
                int expectedOutPut = 45;

                List<List<int>> matrix = new List<List<int>>();
                var sut = new LeagueBackendChallenge();
                var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(validFilePath);
                sut.PrepareMatrix(fileData, matrix);
                var result = sut.SumOfMatrix(matrix);

                Assert.AreEqual(expectedOutPut, result);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, invalidDataErrorMessage);
            }
        }

        [TestMethod]
        public void TryProductOfMatrix_With_ValidMatrix_Should_Pass()
        {
            try
            {
                int expectedOutPut = 362880;

                List<List<int>> matrix = new List<List<int>>();
                var sut = new LeagueBackendChallenge();
                var fileData = sut.ReadFile_Filtered_SpaceOrEmpty(validFilePath);
                sut.PrepareMatrix(fileData, matrix);
                var result = sut.ProductOfMatrix(matrix);

                Assert.AreEqual(expectedOutPut, result);
            }
            catch (Exception ex)
            {
                StringAssert.Contains(ex.Message, invalidDataErrorMessage);
            }
        }
    }
}