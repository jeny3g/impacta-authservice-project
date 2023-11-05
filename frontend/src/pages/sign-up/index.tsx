import {
  ArrowLeft,
  EnvelopeSimple,
  LockSimple,
  User,
  WarningCircle,
} from "@phosphor-icons/react";
import Head from "next/head";
import Image from "next/image";
import Link from "next/link";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";

import { useCreateUserMutation } from "@/api/user/commands/create";
import { Button } from "@/app/components/Button/Button";
import { Input } from "@/app/components/Input/Input";
import { validationMessages } from "@/utils/validationMessages";

import styles from "./signup.module.css";

export default function SignUp() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateUserCommand>();

  const signUpMutation = useCreateUserMutation();
  const { isLoading } = signUpMutation;

  const handleSignUp = async (data: CreateUserCommand) => {
    const mutationPromise = signUpMutation.mutateAsync(data);

    toast.promise(mutationPromise, {
      pending: "Processando...",
      success:
        "Cadastro realizado com sucesso! Verifique seu e-mail para concluir o cadastro.",
      error: "Ocorreu um erro ao cadastrar o usuário. Tente novamente",
    });

    await mutationPromise;
  };

  return (
    <>
      <Head>
        <title>Cadastrar</title>
      </Head>

      <div className={styles.container}>
        <div className={styles.background}></div>
        <div className={styles.register}>
          <div className={styles.animation}>
            <Image
              className={styles.logoImg}
              src={"/logo.png"}
              alt="Logo"
              height={134}
              width={230.03}
              quality={100}
            />
            <h1 className={styles.title}>Faça seu cadastro</h1>
            <form className={styles.form} onSubmit={handleSubmit(handleSignUp)}>
              <Input
                error={!!errors.name}
                errorMessage={errors.name?.message}
                leftIcon={<User size={22} />}
                placeholder="Nome"
                {...register("name", validationMessages.name)}
                aria-invalid={errors.name ? "true" : "false"}
                rightIcon={<WarningCircle size={22} />}
              />

              <Input
                error={!!errors.email}
                errorMessage={errors.email?.message}
                leftIcon={<EnvelopeSimple size={22} />}
                placeholder="E-mail"
                {...register("email", validationMessages.email)}
                aria-invalid={errors.email ? "true" : "false"}
                rightIcon={<WarningCircle size={22} />}
              />

              <div className={styles.buttons}>
                <Input
                  type="password"
                  error={!!errors.password}
                  errorMessage={errors.password?.message}
                  leftIcon={<LockSimple size={22} />}
                  placeholder="Senha"
                  {...register("password", validationMessages.password)}
                  aria-invalid={errors.password ? "true" : "false"}
                  rightIcon={<WarningCircle size={22} />}
                />
                <Button
                  type="submit"
                  placeholder="Cadastrar"
                  disabled={isLoading}
                />
              </div>
            </form>
            <Link href="/login">
              <p className={styles.paragraph}>
                <ArrowLeft className={styles.iconArrowLeft} size={18} /> Voltar
                para login
              </p>
            </Link>
          </div>
        </div>
      </div>
    </>
  );
}
