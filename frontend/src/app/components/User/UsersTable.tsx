import { Card } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

import { useUsers } from "@/app/hooks/useUsers";
import { pageSizeOptions } from "@/utils/constants";

const columns: GridColDef[] = [
  { field: "id", headerName: "ID", flex: 1 },
  { field: "name", headerName: "Nome", flex: 1 },
  { field: "email", headerName: "Email", flex: 1 },
  {
    field: "active",
    headerName: "Ativo",
  },
  { field: "createdAt", headerName: "Criado em", flex: 1 },
  { field: "updateAt", headerName: "Atualizado" },
  {
    field: "actions",
    headerName: "Actions",
    sortable: false,
    width: 150,
    renderCell: (params) => {
      return <a href={`/usuario/${params.row.id}`}>Ver detalhes</a>;
    },
  },
];

export function UsersTable() {
  const { users, isLoading } = useUsers();

  return (
    <Card>
      <div style={{ height: 400, width: "100%" }}>
        <DataGrid
          rows={users}
          autoHeight
          loading={isLoading}
          columns={columns}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 5 },
            },
          }}
          pageSizeOptions={pageSizeOptions}
        />
      </div>
    </Card>
  );
}
