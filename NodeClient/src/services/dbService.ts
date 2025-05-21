import dotenv from 'dotenv';
import sql from 'mssql';

dotenv.config();

let pool: sql.ConnectionPool;

const getPool = async () => {
  if (!pool) {
    pool = await sql.connect(process.env.DB_CONNECTION_STRING!);
  }
  return pool;
};

export const query = async <T = any>(sqlQuery: string, params?: any[]): Promise<T[]> => {
  const pool = await getPool();
  const result = await pool.request().query(sqlQuery);
  return result.recordset;
};
