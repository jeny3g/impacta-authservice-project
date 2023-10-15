import {
  EnvelopeSimple,
  LockSimple,
  SignIn,
  WarningCircle,
} from "@phosphor-icons/react";
import Head from "next/head";
import Image from "next/image";
import Link from "next/link";
import { useRouter } from "next/router";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";

import { useSignInUserMutation } from "@/api/sessions/commands/autenticate";
import { Button } from "@/app/components/Button/Button";
import { Input } from "@/app/components/Input/Input";
import useAuth from "@/app/hooks/useAuth";

import styles from "./login.module.css";

export default function Login() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<AuthenticateSessionCommand>();
  const { signIn } = useAuth();
  const router = useRouter();

  const signInMutation = useSignInUserMutation();
  const { isError, error, isLoading } = signInMutation;

  const handleSignIn = async (data: AuthenticateSessionCommand) => {
    const mutationPromise = signInMutation.mutateAsync(data);

    toast.promise(mutationPromise, {
      pending: "Processando...",
      success: "Login efetuado com sucesso!",
      error: "Ocorreu um erro ao realizar o login. Por favor, tente novamente",
    });

    const response = await mutationPromise;

    signIn(response);

    router.push("/dashboard");
  };

  return (
    <>
      <Head>
        <title>Login</title>
      </Head>

      <div className={styles.container}>
        <div className={styles.login}>
          <div className={styles.animation}>
            <Image
              className={styles.logoImg}
              src={"/logo.png"}
              alt="Logo"
              height={134}
              width={230.03}
            />
            <h1 className={styles.title}>Faça seu login</h1>
            <form className={styles.form} onSubmit={handleSubmit(handleSignIn)}>
              <Input
                error={!!errors.email}
                errorMessage="Email obrigatório"
                leftIcon={<EnvelopeSimple size={22} />}
                placeholder="E-mail"
                {...register("email", { required: true })}
                aria-invalid={errors.email ? "true" : "false"}
                rightIcon={<WarningCircle size={22} />}
              />
              <div className={styles.buttons}>
                <Input
                  type="password"
                  leftIcon={<LockSimple size={22} />}
                  placeholder="Senha"
                  error={!!errors.password}
                  errorMessage="Senha obrigatória"
                  {...register("password", { required: true })}
                  aria-invalid={errors.password ? "true" : "false"}
                  rightIcon={<WarningCircle size={22} />}
                />

                <Button
                  type="submit"
                  disabled={isLoading}
                  placeholder="Entrar"
                />
              </div>
            </form>
            <div className={styles.accountOptions}>
              <Link href="/forgot-password">
                <p className={styles.recoverPassword}>Esqueci minha senha</p>
              </Link>
              <Link href="/sign-up">
                <p className={styles.paragraph}>
                  <SignIn className={styles.iconSignIn} size={18} /> Criar conta
                </p>
              </Link>
            </div>
          </div>
        </div>
        <div className={styles.background}></div>
      </div>
    </>
  );
}
