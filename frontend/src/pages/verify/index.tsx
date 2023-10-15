import { useEffect, useState } from "react";

import Head from "next/head";
import Image from "next/image";
import Link from "next/link";

import { useVerifyUserMutation } from "@/api/user/commands/verify";

import styles from "./verify.module.css";

export default function Verify() {
  const [tokenFound, setTokenFound] = useState<boolean | null>(null);
  const verifyUserMutation = useVerifyUserMutation();

  useEffect(() => {
    const urlSearchParams = new URLSearchParams(window.location.search);
    const token = urlSearchParams.get("token");

    console.log(token);

    if (token) {
      console.log("token", token);
      verifyUserMutation.mutate(token, {
        onSuccess: (response) => {
          console.log(response);
          setTokenFound(true);
        },
        onError: () => {
          setTokenFound(false);
        },
      });
    } else {
      setTokenFound(false);
    }
  }, []);
  return tokenFound === null ? null : (
    <>
      <Head>
        <title>Verificação de email</title>
      </Head>

      {tokenFound ? (
        <div className={styles.container}>
          <div className={styles.content}>
            <Image
              className={styles.img}
              src={"/top-image.png"}
              alt=""
              width={150}
              height={80}
            />
            <h1 className={styles.title}>
              Endereço de email foi confirmado com sucesso!
            </h1>
            <p className={styles.paragraph}>
              Clique no botão abaixo para efetuar o login:
            </p>
            <Link href="/login">
              <button className={styles.button}>Fazer login</button>
            </Link>
            <Image
              className={styles.img}
              src={"/bottom-image.png"}
              alt=""
              width={150}
              height={50}
            />
          </div>
        </div>
      ) : (
        <div className={styles.tokenContainer}>
          <div className={styles.token}>
            <h1 className={styles.tokenTitle}>
              Não foi possivel verificar o usúario
            </h1>
            <p>
              Cadastre-se novamente e verifique seu email para ativar o usúario
            </p>
            <Link href="/sign-up">
              <button className={styles.button}>Cadastrar</button>
            </Link>
          </div>
        </div>
      )}
    </>
  );
}
