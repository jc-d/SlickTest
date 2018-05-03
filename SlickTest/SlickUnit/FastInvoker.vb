Imports System.Reflection
Imports System.Reflection.Emit

'Borrowed from http://forums.asp.net/t/1036046.aspx
Public Delegate Function FastInvokeHandler(ByVal _target As Object, ByVal _params As Object()) As Object

''' <summary>
''' A class to invoke methods using System.Reflection.Emit.DynamicMethod (.NET 2.0).
''' </summary>
Public NotInheritable Class FastMethodInvoker
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Returns DynamicMethod.
    ''' </summary>
    ''' <param name="_methodInfo">MethodInfo.</param>
    ''' <returns>Delegate</returns>
    Public Shared Function GetMethodInvoker(ByVal _methodInfo As MethodInfo) As FastInvokeHandler
        Dim dynamicMethod As New DynamicMethod(String.Empty, GetType(Object), New Type() {GetType(Object), GetType(Object())}, _methodInfo.DeclaringType.[Module])
        Dim il As ILGenerator = dynamicMethod.GetILGenerator()
        Dim ps As ParameterInfo() = _methodInfo.GetParameters()
        Dim paramTypes As Type() = New Type(ps.Length - 1) {}
        For ii As Integer = 0 To paramTypes.Length - 1
            If ps(ii).ParameterType.IsByRef Then
                paramTypes(ii) = ps(ii).ParameterType.GetElementType()
            Else
                paramTypes(ii) = ps(ii).ParameterType
            End If
        Next
        Dim locals As LocalBuilder() = New LocalBuilder(paramTypes.Length - 1) {}

        For ii As Integer = 0 To paramTypes.Length - 1
            locals(ii) = il.DeclareLocal(paramTypes(ii), True)
        Next

        For ii As Integer = 0 To paramTypes.Length - 1
            il.Emit(OpCodes.Ldarg_1)
            EmitFastInt(il, ii)
            il.Emit(OpCodes.Ldelem_Ref)
            EmitCastToReference(il, paramTypes(ii))
            il.Emit(OpCodes.Stloc, locals(ii))
        Next
        il.Emit(OpCodes.Ldarg_0)
        For ii As Integer = 0 To paramTypes.Length - 1
            If ps(ii).ParameterType.IsByRef Then
                il.Emit(OpCodes.Ldloca_S, locals(ii))
            Else
                il.Emit(OpCodes.Ldloc, locals(ii))
            End If
        Next
        il.EmitCall(OpCodes.Callvirt, _methodInfo, Nothing)
        If _methodInfo.ReturnType Is GetType(System.Void) Then
            il.Emit(OpCodes.Ldnull)
        Else
            EmitBoxIfNeeded(il, _methodInfo.ReturnType)
        End If

        For ii As Integer = 0 To paramTypes.Length - 1
            If ps(ii).ParameterType.IsByRef Then
                il.Emit(OpCodes.Ldarg_1)
                EmitFastInt(il, ii)
                il.Emit(OpCodes.Ldloc, locals(ii))
                If locals(ii).LocalType.IsValueType Then
                    il.Emit(OpCodes.Box, locals(ii).LocalType)
                End If
                il.Emit(OpCodes.Stelem_Ref)
            End If
        Next

        il.Emit(OpCodes.Ret)
        Dim invoker As FastInvokeHandler = DirectCast(dynamicMethod.CreateDelegate(GetType(FastInvokeHandler)), FastInvokeHandler)
        Return invoker
    End Function

    ''' <summary>
    ''' Emits the cast to reference.
    ''' </summary>
    ''' <param name="il">The il.</param>
    ''' <param name="type">The type.</param>
    Private Shared Sub EmitCastToReference(ByVal il As ILGenerator, ByVal type As System.Type)
        If type.IsValueType Then
            il.Emit(OpCodes.Unbox_Any, type)
        Else
            il.Emit(OpCodes.Castclass, type)
        End If
    End Sub

    ''' <summary>
    ''' Emits the box if needed.
    ''' </summary>
    ''' <param name="il">The il.</param>
    ''' <param name="type">The type.</param>
    Private Shared Sub EmitBoxIfNeeded(ByVal il As ILGenerator, ByVal type As System.Type)
        If type.IsValueType Then
            il.Emit(OpCodes.Box, type)
        End If
    End Sub

    ''' <summary>
    ''' Emits the fast int.
    ''' </summary>
    ''' <param name="il">The il.</param>
    ''' <param name="value">The value.</param>
    Private Shared Sub EmitFastInt(ByVal il As ILGenerator, ByVal value As Integer)
        Select Case value
            Case -1
                il.Emit(OpCodes.Ldc_I4_M1)
                Return
            Case 0
                il.Emit(OpCodes.Ldc_I4_0)
                Return
            Case 1
                il.Emit(OpCodes.Ldc_I4_1)
                Return
            Case 2
                il.Emit(OpCodes.Ldc_I4_2)
                Return
            Case 3
                il.Emit(OpCodes.Ldc_I4_3)
                Return
            Case 4
                il.Emit(OpCodes.Ldc_I4_4)
                Return
            Case 5
                il.Emit(OpCodes.Ldc_I4_5)
                Return
            Case 6
                il.Emit(OpCodes.Ldc_I4_6)
                Return
            Case 7
                il.Emit(OpCodes.Ldc_I4_7)
                Return
            Case 8
                il.Emit(OpCodes.Ldc_I4_8)
                Return
        End Select

        If value > -129 AndAlso value < 128 Then
            il.Emit(OpCodes.Ldc_I4_S, CType(value, [SByte]))
        Else
            il.Emit(OpCodes.Ldc_I4, value)
        End If
    End Sub
End Class
