using Programmeer_Learning_App;
using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;
using Programmeer_Learning_App.Exporting;

namespace Programmeer_Learning_App_Unit_Tests.Exporting;

public class TXTFileWriterUnitTest
{
    [Fact]
    public void SmallProgramExport()
    {
        // Arrange
        Program program = new Program();
        program.Add(new MoveCommand(4));
        program.Add(new TurnCommand(RelativeDir.Left));

        // Act
        TXTFileWriter.WriteFile(program);

        // Assert
        Assert.True(true);
    }
}