using AkiSister.AkiSisterCode.Relics;

using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Interop.AutoRegistration;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Interop.AutoRegistration;
using AkiSister.Characters;

namespace AkiSister.AkiSisterCode.Relics;
[RegisterRelic(typeof(AkiSisterRelicPool))]

public class BambooShootSoup() : AkiSisterRelic
{
    private bool _isActivating;

    private int _attacksPlayedThisTurn;
    
    public override RelicRarity Rarity =>
        RelicRarity.Rare;

    public override bool ShowCounter => CombatManager.Instance.IsInProgress;

	public override int DisplayAmount
	{
		get
		{
			if (!IsActivating)
			{
				return AttacksPlayedThisTurn % base.DynamicVars.Cards.IntValue;
			}
			return base.DynamicVars.Cards.IntValue;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new CardsVar(3),
		new PowerVar<StrengthPower>(1m),
		new PowerVar<DexterityPower>(1m)
	];

	protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
		HoverTipFactory.FromPower<StrengthPower>(),
		HoverTipFactory.FromPower<DexterityPower>()
	];

	private bool IsActivating
	{
		get
		{
			return _isActivating;
		}
		set
		{
			AssertMutable();
			_isActivating = value;
			UpdateDisplay();
		}
	}

	private int AttacksPlayedThisTurn
	{
		get
		{
			return _attacksPlayedThisTurn;
		}
		set
		{
			AssertMutable();
			_attacksPlayedThisTurn = value;
			UpdateDisplay();
		}
	}

	private void UpdateDisplay()
	{
		if (IsActivating)
		{
			base.Status = RelicStatus.Normal;
		}
		else
		{
			int intValue = base.DynamicVars.Cards.IntValue;
			base.Status = ((AttacksPlayedThisTurn % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
		}
		InvokeDisplayAmountChanged();
	}

	public override Task BeforeCombatStart()
	{
		AttacksPlayedThisTurn = 0;
		base.Status = RelicStatus.Normal;
		return Task.CompletedTask;
	}

	public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
	{
		AttacksPlayedThisTurn = 0;
		base.Status = RelicStatus.Normal;
		return Task.CompletedTask;
	}

	public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		if (cardPlay.Card.Owner != base.Owner || !CombatManager.Instance.IsInProgress || cardPlay.Card.Type != CardType.Status)
		{
			return;
		}
		AttacksPlayedThisTurn++;
		int intValue = base.DynamicVars.Cards.IntValue;
		if (AttacksPlayedThisTurn % intValue == 0)
		{
			Creature creature = base.Owner.RunState.Rng.CombatTargets.NextItem(base.Owner.Creature.CombatState.HittableEnemies);
			if (creature != null)
			{
				TaskHelper.RunSafely(DoActivateVisuals());
				await PowerCmd.Apply<StrengthPower>(choiceContext, base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null);
				await PowerCmd.Apply<DexterityPower>(choiceContext, base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null);
			}
		}
	}

	private async Task DoActivateVisuals()
	{
		IsActivating = true;
		Flash();
		await Cmd.Wait(1f);
		IsActivating = false;
	}

	public override Task AfterCombatEnd(CombatRoom _)
	{
		base.Status = RelicStatus.Normal;
		IsActivating = false;
		return Task.CompletedTask;
	}
}