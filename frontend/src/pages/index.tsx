import Link from "next/link";

export default function Home() {
  return (
    <div>
      Ola, sou a pagina inicial
      <Link href="/login">
        <button>Clique aqui para Logar</button>
      </Link>
    </div>
  );
}
