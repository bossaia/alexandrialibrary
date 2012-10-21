﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LotR.Cards.Encounter.Enemies;
using LotR.Effects;
using LotR.Effects.Choices;
using LotR.Effects.Payments;
using LotR.Effects.Phases;
using LotR.Effects.Phases.Any;
using LotR.Effects.Phases.Combat;
using LotR.States;
using LotR.States.Areas;
using LotR.States.Phases.Any;
using LotR.States.Phases.Combat;

namespace LotR.Cards.Player.Heroes
{
    public class Dunhere
        : HeroCardBase
    {
        public Dunhere()
            : base("Dunhere", CardSet.Core, 9, Sphere.Spirit, 8, 1, 2, 1, 4)
        {
            AddTrait(Trait.Rohan);
            AddTrait(Trait.Warrior);

            AddEffect(new CanAttackEnemiesInTheStagingArea(this));
            AddEffect(new PlusOneAttackWhenAttackingAnEnemyInTheStagingArea(this));
        }

        public class CanAttackEnemiesInTheStagingArea
            : PassiveCharacterAbilityBase, IBeforeChoosingEnemyToAttack
        {
            public CanAttackEnemiesInTheStagingArea(Dunhere source)
                : base("Dunhere can target enemies in the staging area when he attacks alone. When doing so, he gets +1 Attack.", source)
            {
            }

            public void BeforeChoosingEnemyToAttack(IGame game)
            {
                var combatPhase = game.CurrentPhase as ICombatPhase;
                if (combatPhase == null)
                    return;

                var chooseEnemy = game.CurrentPhase.GetEnemiesChosenToAttack().FirstOrDefault();
                if (chooseEnemy == null)
                    return;

                if (chooseEnemy.Attackers.Count() != 1)
                    return;

                var attacker = chooseEnemy.Attackers.Where(x => x.Card.Id == Source.Id).FirstOrDefault();
                if (attacker == null)
                    return;

                game.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var chooseEnemy = game.CurrentPhase.GetEnemiesChosenToAttack().FirstOrDefault();
                if (chooseEnemy == null)
                    return;

                foreach (var enemy in game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>())
                {
                    chooseEnemy.AddEnemy(enemy);
                }
            }
        }

        public class PlusOneAttackWhenAttackingAnEnemyInTheStagingArea
            : PassiveCharacterAbilityBase, IDuringDetermineAttack
        {
            public PlusOneAttackWhenAttackingAnEnemyInTheStagingArea(Dunhere source)
                : base("When attacking an enemy in the staging area, Dunhere gets +1 Attack.", source)
            {
            }

            public void DuringDetermineAttack(IDetermineAttack determineAttack)
            {
                if (determineAttack.Attacker.Card.Id != Source.Id)
                    return;

                var enemy = determineAttack.Game.StagingArea.CardsInStagingArea.OfType<IEnemyInPlay>().Where(x => x.Card.Id == determineAttack.Defender.Card.Id).FirstOrDefault();
                if (enemy == null)
                    return;

                determineAttack.Game.AddEffect(this);
            }

            public override void Resolve(IGame game, IPayment payment, IChoice choice)
            {
                var determineAttack = game.CurrentPhase.GetDetermineAttacks().FirstOrDefault();
                if (determineAttack == null)
                    return;

                determineAttack.Attack += 1;
            }
        }
    }
}
