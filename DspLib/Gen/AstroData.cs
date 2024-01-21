using DspLib.Algorithms;
using DspLib.Enum;

public struct AstroData
{
    public int id;
    public EAstroType type;
    public int parentId;
    public float uRadius;
    public Quaternion uRot;
    public Quaternion uRotNext;
    public VectorLF3 uPos;
    public VectorLF3 uPosNext;

    public void PositionU(ref VectorLF3 lpos, out VectorLF3 upos)
    {
        var num1 = 2.0 * lpos.x;
        var num2 = 2.0 * lpos.y;
        var num3 = 2.0 * lpos.z;
        var num4 = uRot.w * (double)uRot.w - 0.5;
        var num5 = uRot.x * num1 + uRot.y * num2 + uRot.z * num3;
        upos.x = num1 * num4 + (uRot.y * num3 - uRot.z * num2) * uRot.w + uRot.x * num5 + uPos.x;
        upos.y = num2 * num4 + (uRot.z * num1 - uRot.x * num3) * uRot.w + uRot.y * num5 + uPos.y;
        upos.z = num3 * num4 + (uRot.x * num2 - uRot.y * num1) * uRot.w + uRot.z * num5 + uPos.z;
    }

    public void PositionU(ref Vector3 lpos, out VectorLF3 upos)
    {
        var num1 = 2f * lpos.x;
        var num2 = 2f * lpos.y;
        var num3 = 2f * lpos.z;
        var num4 = (float)(uRot.w * (double)uRot.w - 0.5);
        var num5 = (float)(uRot.x * (double)num1 + uRot.y * (double)num2 + uRot.z * (double)num3);
        upos.x = num1 * (double)num4 + (uRot.y * (double)num3 - uRot.z * (double)num2) * uRot.w +
                 uRot.x * (double)num5 + uPos.x;
        upos.y = num2 * (double)num4 + (uRot.z * (double)num1 - uRot.x * (double)num3) * uRot.w +
                 uRot.y * (double)num5 + uPos.y;
        upos.z = num3 * (double)num4 + (uRot.x * (double)num2 - uRot.y * (double)num1) * uRot.w +
                 uRot.z * (double)num5 + uPos.z;
    }

    public void VelocityU(ref VectorLF3 lpos, out Vector3 uvel)
    {
        var num1 = 2.0 * lpos.x;
        var num2 = 2.0 * lpos.y;
        var num3 = 2.0 * lpos.z;
        var num4 = uRot.w * (double)uRot.w - 0.5;
        var num5 = uRot.x * num1 + uRot.y * num2 + uRot.z * num3;
        var num6 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num7 = uRotNext.x * num1 + uRotNext.y * num2 + uRotNext.z * num3;
        uvel.x = (float)(num1 * num6 + (uRotNext.y * num3 - uRotNext.z * num2) * uRotNext.w + uRotNext.x * num7 +
            uPosNext.x - (num1 * num4 + (uRot.y * num3 - uRot.z * num2) * uRot.w + uRot.x * num5 + uPos.x)) * 60f;
        uvel.y = (float)(num2 * num6 + (uRotNext.z * num1 - uRotNext.x * num3) * uRotNext.w + uRotNext.y * num7 +
            uPosNext.y - (num2 * num4 + (uRot.z * num1 - uRot.x * num3) * uRot.w + uRot.y * num5 + uPos.y)) * 60f;
        uvel.z = (float)(num3 * num6 + (uRotNext.x * num2 - uRotNext.y * num1) * uRotNext.w + uRotNext.z * num7 +
            uPosNext.z - (num3 * num4 + (uRot.x * num2 - uRot.y * num1) * uRot.w + uRot.z * num5 + uPos.z)) * 60f;
    }

    public void VelocityU(ref Vector3 lpos, out Vector3 uvel)
    {
        var num1 = 2.0 * lpos.x;
        var num2 = 2.0 * lpos.y;
        var num3 = 2.0 * lpos.z;
        var num4 = uRot.w * (double)uRot.w - 0.5;
        var num5 = uRot.x * num1 + uRot.y * num2 + uRot.z * num3;
        var num6 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num7 = uRotNext.x * num1 + uRotNext.y * num2 + uRotNext.z * num3;
        uvel.x = (float)(num1 * num6 + (uRotNext.y * num3 - uRotNext.z * num2) * uRotNext.w + uRotNext.x * num7 +
            uPosNext.x - (num1 * num4 + (uRot.y * num3 - uRot.z * num2) * uRot.w + uRot.x * num5 + uPos.x)) * 60f;
        uvel.y = (float)(num2 * num6 + (uRotNext.z * num1 - uRotNext.x * num3) * uRotNext.w + uRotNext.y * num7 +
            uPosNext.y - (num2 * num4 + (uRot.z * num1 - uRot.x * num3) * uRot.w + uRot.y * num5 + uPos.y)) * 60f;
        uvel.z = (float)(num3 * num6 + (uRotNext.x * num2 - uRotNext.y * num1) * uRotNext.w + uRotNext.z * num7 +
            uPosNext.z - (num3 * num4 + (uRot.x * num2 - uRot.y * num1) * uRot.w + uRot.z * num5 + uPos.z)) * 60f;
    }

    public void VelocityL2U(ref Vector3 lpos, ref Vector3 lvel, out Vector3 uvel)
    {
        var num1 = 2f * lvel.x;
        var num2 = 2f * lvel.y;
        var num3 = 2f * lvel.z;
        var num4 = (float)(uRot.w * (double)uRot.w - 0.5);
        var num5 = (float)(uRot.x * (double)num1 + uRot.y * (double)num2 + uRot.z * (double)num3);
        uvel.x = (float)(num1 * (double)num4 + (uRot.y * (double)num3 - uRot.z * (double)num2) * uRot.w +
                         uRot.x * (double)num5);
        uvel.y = (float)(num2 * (double)num4 + (uRot.z * (double)num1 - uRot.x * (double)num3) * uRot.w +
                         uRot.y * (double)num5);
        uvel.z = (float)(num3 * (double)num4 + (uRot.x * (double)num2 - uRot.y * (double)num1) * uRot.w +
                         uRot.z * (double)num5);
        var num6 = 2.0 * lpos.x;
        var num7 = 2.0 * lpos.y;
        var num8 = 2.0 * lpos.z;
        var num9 = uRot.w * (double)uRot.w - 0.5;
        var num10 = uRot.x * num6 + uRot.y * num7 + uRot.z * num8;
        var num11 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num12 = uRotNext.x * num6 + uRotNext.y * num7 + uRotNext.z * num8;
        uvel.x += (float)(num6 * num11 + (uRotNext.y * num8 - uRotNext.z * num7) * uRotNext.w + uRotNext.x * num12 +
            uPosNext.x - (num6 * num9 + (uRot.y * num8 - uRot.z * num7) * uRot.w + uRot.x * num10 + uPos.x)) * 60f;
        uvel.y += (float)(num7 * num11 + (uRotNext.z * num6 - uRotNext.x * num8) * uRotNext.w + uRotNext.y * num12 +
            uPosNext.y - (num7 * num9 + (uRot.z * num6 - uRot.x * num8) * uRot.w + uRot.y * num10 + uPos.y)) * 60f;
        uvel.z += (float)(num8 * num11 + (uRotNext.x * num7 - uRotNext.y * num6) * uRotNext.w + uRotNext.z * num12 +
            uPosNext.z - (num8 * num9 + (uRot.x * num7 - uRot.y * num6) * uRot.w + uRot.z * num10 + uPos.z)) * 60f;
    }

    public void VelocityL2U(ref VectorLF3 lpos, ref Vector3 lvel, out Vector3 uvel)
    {
        var num1 = 2f * lvel.x;
        var num2 = 2f * lvel.y;
        var num3 = 2f * lvel.z;
        var num4 = (float)(uRot.w * (double)uRot.w - 0.5);
        var num5 = (float)(uRot.x * (double)num1 + uRot.y * (double)num2 + uRot.z * (double)num3);
        uvel.x = (float)(num1 * (double)num4 + (uRot.y * (double)num3 - uRot.z * (double)num2) * uRot.w +
                         uRot.x * (double)num5);
        uvel.y = (float)(num2 * (double)num4 + (uRot.z * (double)num1 - uRot.x * (double)num3) * uRot.w +
                         uRot.y * (double)num5);
        uvel.z = (float)(num3 * (double)num4 + (uRot.x * (double)num2 - uRot.y * (double)num1) * uRot.w +
                         uRot.z * (double)num5);
        var num6 = 2.0 * lpos.x;
        var num7 = 2.0 * lpos.y;
        var num8 = 2.0 * lpos.z;
        var num9 = uRot.w * (double)uRot.w - 0.5;
        var num10 = uRot.x * num6 + uRot.y * num7 + uRot.z * num8;
        var num11 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num12 = uRotNext.x * num6 + uRotNext.y * num7 + uRotNext.z * num8;
        uvel.x += (float)(num6 * num11 + (uRotNext.y * num8 - uRotNext.z * num7) * uRotNext.w + uRotNext.x * num12 +
            uPosNext.x - (num6 * num9 + (uRot.y * num8 - uRot.z * num7) * uRot.w + uRot.x * num10 + uPos.x)) * 60f;
        uvel.y += (float)(num7 * num11 + (uRotNext.z * num6 - uRotNext.x * num8) * uRotNext.w + uRotNext.y * num12 +
            uPosNext.y - (num7 * num9 + (uRot.z * num6 - uRot.x * num8) * uRot.w + uRot.y * num10 + uPos.y)) * 60f;
        uvel.z += (float)(num8 * num11 + (uRotNext.x * num7 - uRotNext.y * num6) * uRotNext.w + uRotNext.z * num12 +
            uPosNext.z - (num8 * num9 + (uRot.x * num7 - uRot.y * num6) * uRot.w + uRot.z * num10 + uPos.z)) * 60f;
    }

    public void VelocityL2U(
        ref VectorLF3 lpos,
        ref Vector3 lvel,
        out Vector3 uvel_astro,
        out Vector3 uvel_obj)
    {
        var num1 = 2f * lvel.x;
        var num2 = 2f * lvel.y;
        var num3 = 2f * lvel.z;
        var num4 = (float)(uRot.w * (double)uRot.w - 0.5);
        var num5 = (float)(uRot.x * (double)num1 + uRot.y * (double)num2 + uRot.z * (double)num3);
        uvel_obj.x = (float)(num1 * (double)num4 + (uRot.y * (double)num3 - uRot.z * (double)num2) * uRot.w +
                             uRot.x * (double)num5);
        uvel_obj.y = (float)(num2 * (double)num4 + (uRot.z * (double)num1 - uRot.x * (double)num3) * uRot.w +
                             uRot.y * (double)num5);
        uvel_obj.z = (float)(num3 * (double)num4 + (uRot.x * (double)num2 - uRot.y * (double)num1) * uRot.w +
                             uRot.z * (double)num5);
        var num6 = 2.0 * lpos.x;
        var num7 = 2.0 * lpos.y;
        var num8 = 2.0 * lpos.z;
        var num9 = uRot.w * (double)uRot.w - 0.5;
        var num10 = uRot.x * num6 + uRot.y * num7 + uRot.z * num8;
        var num11 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num12 = uRotNext.x * num6 + uRotNext.y * num7 + uRotNext.z * num8;
        uvel_astro.x = (float)(num6 * num11 + (uRotNext.y * num8 - uRotNext.z * num7) * uRotNext.w +
                               uRotNext.x * num12 + uPosNext.x -
                               (num6 * num9 + (uRot.y * num8 - uRot.z * num7) * uRot.w + uRot.x * num10 + uPos.x)) *
                       60f;
        uvel_astro.y = (float)(num7 * num11 + (uRotNext.z * num6 - uRotNext.x * num8) * uRotNext.w +
                               uRotNext.y * num12 + uPosNext.y -
                               (num7 * num9 + (uRot.z * num6 - uRot.x * num8) * uRot.w + uRot.y * num10 + uPos.y)) *
                       60f;
        uvel_astro.z = (float)(num8 * num11 + (uRotNext.x * num7 - uRotNext.y * num6) * uRotNext.w +
                               uRotNext.z * num12 + uPosNext.z -
                               (num8 * num9 + (uRot.x * num7 - uRot.y * num6) * uRot.w + uRot.z * num10 + uPos.z)) *
                       60f;
    }

    public void VelocityU2L(ref Vector3 lpos, ref Vector3 uvel, out Vector3 lvel)
    {
        var num1 = 2.0 * lpos.x;
        var num2 = 2.0 * lpos.y;
        var num3 = 2.0 * lpos.z;
        var num4 = uRot.w * (double)uRot.w - 0.5;
        var num5 = uRot.x * num1 + uRot.y * num2 + uRot.z * num3;
        var num6 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num7 = uRotNext.x * num1 + uRotNext.y * num2 + uRotNext.z * num3;
        Vector3 vector3_1;
        vector3_1.x = (float)(num1 * num6 + (uRotNext.y * num3 - uRotNext.z * num2) * uRotNext.w + uRotNext.x * num7 +
            uPosNext.x - (num1 * num4 + (uRot.y * num3 - uRot.z * num2) * uRot.w + uRot.x * num5 + uPos.x)) * 60f;
        vector3_1.y = (float)(num2 * num6 + (uRotNext.z * num1 - uRotNext.x * num3) * uRotNext.w + uRotNext.y * num7 +
            uPosNext.y - (num2 * num4 + (uRot.z * num1 - uRot.x * num3) * uRot.w + uRot.y * num5 + uPos.y)) * 60f;
        vector3_1.z = (float)(num3 * num6 + (uRotNext.x * num2 - uRotNext.y * num1) * uRotNext.w + uRotNext.z * num7 +
            uPosNext.z - (num3 * num4 + (uRot.x * num2 - uRot.y * num1) * uRot.w + uRot.z * num5 + uPos.z)) * 60f;
        var vector3_2 = new Vector3(uvel.x - vector3_1.x, uvel.y - vector3_1.y, uvel.z - vector3_1.z);
        var num8 = 2.0 * vector3_2.x;
        var num9 = 2.0 * vector3_2.y;
        var num10 = 2.0 * vector3_2.z;
        var num11 = uRot.w * (double)uRot.w - 0.5;
        var num12 = uRot.x * num8 + uRot.y * num9 + uRot.z * num10;
        lvel.x = (float)(num8 * num11 - (uRot.y * num10 - uRot.z * num9) * uRot.w + uRot.x * num12);
        lvel.y = (float)(num9 * num11 - (uRot.z * num8 - uRot.x * num10) * uRot.w + uRot.y * num12);
        lvel.z = (float)(num10 * num11 - (uRot.x * num9 - uRot.y * num8) * uRot.w + uRot.z * num12);
    }

    public void VelocityU2L(ref VectorLF3 lpos, ref Vector3 uvel, out Vector3 lvel)
    {
        var num1 = 2.0 * lpos.x;
        var num2 = 2.0 * lpos.y;
        var num3 = 2.0 * lpos.z;
        var num4 = uRot.w * (double)uRot.w - 0.5;
        var num5 = uRot.x * num1 + uRot.y * num2 + uRot.z * num3;
        var num6 = uRotNext.w * (double)uRotNext.w - 0.5;
        var num7 = uRotNext.x * num1 + uRotNext.y * num2 + uRotNext.z * num3;
        Vector3 vector3_1;
        vector3_1.x = (float)(num1 * num6 + (uRotNext.y * num3 - uRotNext.z * num2) * uRotNext.w + uRotNext.x * num7 +
            uPosNext.x - (num1 * num4 + (uRot.y * num3 - uRot.z * num2) * uRot.w + uRot.x * num5 + uPos.x)) * 60f;
        vector3_1.y = (float)(num2 * num6 + (uRotNext.z * num1 - uRotNext.x * num3) * uRotNext.w + uRotNext.y * num7 +
            uPosNext.y - (num2 * num4 + (uRot.z * num1 - uRot.x * num3) * uRot.w + uRot.y * num5 + uPos.y)) * 60f;
        vector3_1.z = (float)(num3 * num6 + (uRotNext.x * num2 - uRotNext.y * num1) * uRotNext.w + uRotNext.z * num7 +
            uPosNext.z - (num3 * num4 + (uRot.x * num2 - uRot.y * num1) * uRot.w + uRot.z * num5 + uPos.z)) * 60f;
        var vector3_2 = new Vector3(uvel.x - vector3_1.x, uvel.y - vector3_1.y, uvel.z - vector3_1.z);
        var num8 = 2.0 * vector3_2.x;
        var num9 = 2.0 * vector3_2.y;
        var num10 = 2.0 * vector3_2.z;
        var num11 = uRot.w * (double)uRot.w - 0.5;
        var num12 = uRot.x * num8 + uRot.y * num9 + uRot.z * num10;
        lvel.x = (float)(num8 * num11 - (uRot.y * num10 - uRot.z * num9) * uRot.w + uRot.x * num12);
        lvel.y = (float)(num9 * num11 - (uRot.z * num8 - uRot.x * num10) * uRot.w + uRot.y * num12);
        lvel.z = (float)(num10 * num11 - (uRot.x * num9 - uRot.y * num8) * uRot.w + uRot.z * num12);
    }

    public void SetEmpty()
    {
        id = 0;
        type = EAstroType.None;
        parentId = 0;
        uRadius = 0.0f;
        uRot.x = uRot.y = uRot.z = 0.0f;
        uRotNext.x = uRotNext.y = uRotNext.z = 0.0f;
        uRot.w = uRotNext.w = 1f;
        uPos.x = uPos.y = uPos.z = 0.0;
        uPosNext.x = uPosNext.y = uPosNext.z = 0.0;
    }

    public static string IdString(int id)
    {
        if (id == 0)
            return "0";
        if (id > 1000000)
            return string.Format("S{0}", id - 1000000);
        return id <= 204899 ? string.Format("G{0}", id) : "??";
    }
}