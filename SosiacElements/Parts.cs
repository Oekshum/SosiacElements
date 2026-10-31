using Parts;
using Quintessential;
using static Quintessential.PartCycleDelegate;

namespace SosiacElements
{
    internal class Parts
    {
        public static PartType TotalAntithesis, PartialAntithesis;

        public static void LoadPartTypes()
        {
            TotalAntithesis = new()
            {
                cost = 25,
                isFullHexCover = true,
                glowTexture = AssetLoaderHelper.LoadTexture("textures/sosic_elements/select/straight_glow"),
                strokeTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/select/straight_stroke"),
                baseTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_icon"),
                hoverTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_icon"),
                glyphHexes = new HexIndex[]
            {
                AntithesisGlyph.InputLeftHex,
                AntithesisGlyph.InputRightHex,
                AntithesisGlyph.OutputHex,
            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(SosiacElements.Instance.GetIdentifier("antithesis"))
            };

            SosiacElements.Instance.AddPartType(TotalAntithesis, "antithesis", AntithesisGlyph.RenderTotal);
            QApi.AddPartTypeToPanel(TotalAntithesis, PartTypes.proliferationGlyph);
            SosiacElements.Instance.AddPuzzlePermission("antithesis");
            QApi.BindGlyphCycle(TotalAntithesis, new(PartCycleExecutionType.Normal, AntithesisGlyph.OnCycleTotal));
            SosiacElements.Instance.AddRecipe(new(), TotalAntithesis.Id, TotalAntithesis.Id);

            PartialAntithesis = new()
            {
                cost = 15,
                isFullHexCover = true,
                glowTexture = AssetLoaderHelper.LoadTexture("textures/sosic_elements/select/straight_glow"),
                strokeTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/select/straight_stroke"),
                baseTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_icon"),
                hoverTexture = AssetLoaderHelper.LoadTexture("textures/sosiac_elements/parts/antithesis_icon"),
                glyphHexes = new HexIndex[]
            {
                AntithesisGlyph.InputRightHex,
                AntithesisGlyph.OutputHex,
            },
                permissionCategory = PuzzlePermissions.None,
                CustomPermissionCheck = perms => perms.Contains(SosiacElements.Instance.GetIdentifier("partial_antithesis"))
            };

            SosiacElements.Instance.AddPartType(PartialAntithesis, "partial_antithesis", AntithesisGlyph.RenderPartial);
            QApi.AddPartTypeToPanel(PartialAntithesis, PartTypes.proliferationGlyph);
            SosiacElements.Instance.AddPuzzlePermission("partial_antithesis");
            QApi.BindGlyphCycle(PartialAntithesis, new(PartCycleExecutionType.Normal, AntithesisGlyph.OnCyclePartial));
            SosiacElements.Instance.AddRecipe(new(), PartialAntithesis.Id, PartialAntithesis.Id);
        }
    }
}