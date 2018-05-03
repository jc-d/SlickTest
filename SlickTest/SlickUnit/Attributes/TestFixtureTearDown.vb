''' <summary>
''' Cleanup the class, ran once per class.
''' </summary>
''' <remarks></remarks>
<AttributeUsage(AttributeTargets.Method)> _
Public Class TestFixtureTearDown
    Inherits GenericMethodAttribute

End Class



'NUnit
'TestFixtureSetUp 
'SetUp 
'Test 
'TearDown 
'Repeat steps 2, 3, and 4 for each test that's being run in this fixture. 
'TestFixtureTearDown